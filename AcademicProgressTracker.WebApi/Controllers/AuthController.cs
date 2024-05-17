using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Application.Common.ViewModels.Auth;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly AcademicProgressDataContext _dataContext;

        public AuthController(IAuthService authService, AcademicProgressDataContext dataContext)
        {
            _authService = authService;
            _dataContext = dataContext;
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterStudentViewModel request)
        {
            var registeredUser = await _authService.RegisterStudentUserAsync(request);

            return Created();
        }

        /// <summary>
        /// Обеспечивает вход пользователя в систему
        /// </summary>
        /// <param name="request"></param>
        /// <returns>access-токен</returns>
        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] UserDto request)
        {
            var authTokens = await _authService.LoginAsync(request);
            SetRefreshTokenToCookies(authTokens.RefreshToken);
            var accessToken = authTokens.AccessToken.Token;

            return Ok(accessToken);
        }

        /// <summary>
        /// Обновление access и refresh токенов
        /// </summary>
        /// <returns>access-токен</returns>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var newAuthTokens = await _authService.UpdateRefreshTokenAsync(refreshToken);
            SetRefreshTokenToCookies(newAuthTokens.RefreshToken);
            var accessToken = newAuthTokens.AccessToken.Token;

            return Ok(accessToken);
        }

        /// <summary>
        /// Получение access-токена на 48 часов
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>access-токен</returns>
        [HttpGet("access-token-48-hours/{userId}")]
        public async Task<ActionResult<string>> GetAuthTokenFor48Hours(Guid userId)
        {
            var authToken = await _authService.GetAccessTokenFor48HoursAsync(userId);
            return Ok(authToken.Token);
        }

        /// <summary>
        /// Изменение данных аккаунта с помощью access токена
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [HttpPut("change-account-data/{accessToken}")]
        public async Task<ActionResult> ChangeAccountDataByAccessToken(string accessToken, UserDto userDto)
        {
            await _authService.ChangeAccountDataByAccessToken(accessToken, userDto);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUserById(Guid userId)
        {
            var user = await _dataContext.Users.SingleOrDefaultAsync(user => user.Id == userId);
            if (user == null)
                throw new BadHttpRequestException("Студент с таким id не найден!");

            _dataContext.Remove(user);
            await _dataContext.SaveChangesAsync();
            return Ok();
        }

        private void SetRefreshTokenToCookies(RefreshToken refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                // HttpOnly = true для того, чтобы в клиенте невозможно было получить refresh token через скрипты js
                HttpOnly = true,
                Expires = refreshToken.Expires,
                SameSite = SameSiteMode.None,
                Secure = true // Важно для HTTPS
            };

            // Добавляем cookie с именем refreshToken
            Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
        }
    }
}
