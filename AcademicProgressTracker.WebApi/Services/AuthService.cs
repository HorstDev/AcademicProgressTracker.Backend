using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Application.Common.ViewModels.Auth;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace AcademicProgressTracker.WebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _passwordHasher;

        public AuthService(
            IConfiguration configuration, IUserRepository userRepository,
            IRoleRepository roleRepository, IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<AccessToken> GetAccessTokenFor48HoursAsync(Guid userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new BadHttpRequestException("Пользователь не найден!");
                }
                var authToken = CreateJwt(user, 48 * 60);
                return authToken;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ChangeAccountDataByAccessToken(string accessToken, UserDto userDto)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.ReadJwtToken(accessToken);
                Guid userId = new Guid(token.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")!.Value);
                var user = await _userRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new BadHttpRequestException("Пользователь не найден!");

                // Проверяем логин и пароль на корректность
                if (string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
                    throw new BadHttpRequestException("Некорректный логин или пароль!");

                // Проверяем, есть ли аккаунт с таким email
                var checkAccount = await _userRepository.GetByEmailAsync(userDto.Email);
                if (checkAccount != null)
                    throw new BadHttpRequestException("Пользователь с такими данными уже существует!");

                // Проверяем, истек ли токен
                if (token.ValidTo < DateTime.Now)
                    throw new BadHttpRequestException("Действие токена истекло!");

                _passwordHasher.CreateHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.Email = userDto.Email;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await _userRepository.UpdateAsync(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> RegisterStudentUserAsync(RegisterStudentViewModel request)
        {
            try
            {
                User? user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null)
                {
                    _passwordHasher.CreateHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user = new User(request.Email, passwordHash, passwordSalt);
                    Role userRole = (await _roleRepository.GetByNameAsync("Student"))!;
                    user.Roles.Add(userRole);
                    
                    user = await _userRepository.CreateAsync(user);

                    return user;
                }
                else
                {
                    throw new BadHttpRequestException("Пользователь с такими данными уже существует!");
                }
            }
            catch (Exception ex)
            {
                // тут можно реализовать логгирование
                throw; // перебрасываем исключение для того, чтобы его поймал middleware
            }
        }

        // Получение пользователя студента с установленным логином и паролем 12345
        // P.S. сейчас используется логин и пароль == name, это для удобства, пока нет отправки ссылки с токеном преподавателю для смены пароля,
        // чтобы можно было заходить за студента во время разработки. ЭТО ИСПРАВИТЬ В БУДУЩЕМ ЧТОБЫ БЫЛИ РАНДОМНЫЕ ДАННЫЕ
        public async Task<User> GetStudentUserWithRandomLoginAndPasswordAsync(string name)
        {
            _passwordHasher.CreateHash(new Guid().ToString(), out byte[] passwordHash, out byte[] passwordSalt);
            Role studentRole = (await _roleRepository.GetByNameAsync("Student"))!;

            var user = new User(name, passwordHash, passwordSalt);
            user.Roles.Add(studentRole);

            user.Profiles.Add(new StudentProfile { User = user, Name = user.Email });

            return user;
        }

        // Получение пользователя преподавателя с установленным рандомным логином и паролем
        // P.S. сейчас используется логин и пароль == name, это для удобства, пока нет отправки ссылки с токеном преподавателю для смены пароля,
        // чтобы можно было заходить за преподавателя во время разработки. ЭТО ИСПРАВИТЬ В БУДУЩЕМ ЧТОБЫ БЫЛИ РАНДОМНЫЕ ДАННЫЕ
        public async Task<User> GetTeacherUserWithRandomLoginAndPasswordAsync(string name)
        {
            _passwordHasher.CreateHash(new Guid().ToString(), out byte[] passwordHash, out byte[] passwordSalt);
            Role teacherRole = (await _roleRepository.GetByNameAsync("Teacher"))!;

            var user = new User(name, passwordHash, passwordSalt);
            user.Roles.Add(teacherRole);

            user.Profiles.Add(new TeacherProfile { User = user, Name = user.Email });

            return user;
        }

        public async Task<AuthTokensDto> LoginAsync(UserDto request)
        {
            try
            {
                User? user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null)
                {
                    throw new BadHttpRequestException("Пользователь не найден!");
                }

                if (!_passwordHasher.VerifyHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    throw new BadHttpRequestException("Неверный пароль!");
                }

                var accessToken = CreateJwt(user, 30);
                var refreshToken = new RefreshToken(30);

                // При каждой авторизации пользователя обновляем ему refresh token в бд
                user.RefreshToken = refreshToken.Token;
                user.TokenCreated = refreshToken.Created;
                user.TokenExpires = refreshToken.Expires;
                await _userRepository.UpdateAsync(user);

                return new AuthTokensDto(accessToken, refreshToken);
            }
            catch (Exception ex)
            {
                // тут можно реализовать логгирование
                throw; // перебрасываем исключение для того, чтобы его поймал middleware
            }
        }

        public async Task<AuthTokensDto> UpdateRefreshTokenAsync(string? oldRefreshToken)
        {
            try
            {
                //var refreshToken = Request.Cookies["refreshToken"];
                var user = await _userRepository.GetByRefreshTokenAsync(oldRefreshToken); // P.S. наверное надо выставить индексы на refresh токен

                // Если пользователя с таким refresh токеном не найдено (т.е. странные ситуации, когда неавторизованный пользователь зачем-то хочет refresh токен)
                if (user == null)
                {
                    throw new UnauthorizedAccessException("Некорректный refresh токен!");
                }
                else if (user.TokenExpired())
                {
                    throw new UnauthorizedAccessException("Срок действия refresh токена истек!");
                }

                var accessToken = CreateJwt(user, 30);
                var newRefreshToken = new RefreshToken(30);

                // Устанавливаем новый refresh токен в бд
                user.RefreshToken = newRefreshToken.Token;
                user.TokenCreated = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;
                await _userRepository.UpdateAsync(user);

                return new AuthTokensDto(accessToken, newRefreshToken);
            }
            catch (Exception ex)
            {
                // тут можно реализовать логгирование
                throw; // перебрасываем исключение для того, чтобы его поймал middleware
            }
        }

        private AccessToken CreateJwt(User user, int lifeTimeInMinutes)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:JwtSecret").Value!));

            var accessToken = new AccessToken(user, key, lifeTimeInMinutes);

            return accessToken;
        }
    }
}
