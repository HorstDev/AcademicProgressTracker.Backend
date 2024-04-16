using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Application.Common.ViewModels.Auth;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterAsync([FromBody] RegisterStudentViewModel request)
        {
            var registeredUser = await _authService.RegisterStudentUserAsync(request);

            return Created();
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginAsync([FromBody] UserDto request)
        {
            var authTokens = await _authService.LoginAsync(request);
            SetRefreshTokenToCookies(authTokens.RefreshToken);
            var accessToken = authTokens.AccessToken.Token;

            return Ok(accessToken);
        }

        // Этот метод мы вызываем, когда клиент понял, что истекает или уже истек срок действия access токена и надо выдать новую пару access и refresh токена
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshTokenAsync()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var newAuthTokens = await _authService.UpdateRefreshTokenAsync(refreshToken);
            SetRefreshTokenToCookies(newAuthTokens.RefreshToken);
            var accessToken = newAuthTokens.AccessToken.Token;

            return Ok(accessToken);
        }

        // Получение access-токена на 48 часов
        [HttpGet("access-token-48-hours/{userId}")]
        public async Task<ActionResult<string>> GetAuthTokenFor48Hours(Guid userId)
        {
            var authToken = await _authService.GetAccessTokenFor48HoursAsync(userId);
            return Ok(authToken.Token);
        }

        // Изменение данных аккаунта с помощью access токена
        [HttpPut("change-account-data/{accessToken}")]
        public async Task<ActionResult> ChangeAccountDataByAccessToken(string accessToken, UserDto userDto)
        {
            await _authService.ChangeAccountDataByAccessToken(accessToken, userDto);
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
