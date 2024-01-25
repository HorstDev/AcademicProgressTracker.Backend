using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<User> RegisterAsync(UserDto request)
        {
            try
            {
                User? user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null)
                {
                    _passwordHasher.CreateHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                    user = new User
                    {
                        Email = request.Email,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };
                    Role userRole = (await _roleRepository.GetByNameAsync("Student"))!;
                    user.Role = userRole;
                    await _userRepository.CreateAsync(user);

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

                var accessToken = CreateJwt(user);
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

                var accessToken = CreateJwt(user);
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

        private AccessToken CreateJwt(User user)
        {
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:JwtSecret").Value!));

            var accessToken = new AccessToken(user, key, 30);

            return accessToken;
        }
    }
}
