using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Domain;
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

        [HttpGet, Authorize(Roles = "Student")]
        public ActionResult<string> GetMe()
        {
            return Ok("gdfgdfgdfgdfgdfgdfg");
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto request)
        {
            var registeredUser = await _authService.Register(request);

            return CreatedAtAction(nameof(Register), new { id = registeredUser.Id }, registeredUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserDto request)
        {
            var authTokens = await _authService.Login(request);
            SetRefreshTokenToCookies(authTokens.RefreshToken);
            var accessToken = authTokens.AccessToken.Token;

            return Ok(accessToken);
        }

        // Этот метод мы вызываем, когда клиент понял, что истекает или уже истек срок действия access токена и надо выдать новую пару access и refresh токена
        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var newAuthTokens = await _authService.UpdateRefreshToken(refreshToken);
            SetRefreshTokenToCookies(newAuthTokens.RefreshToken);
            var accessToken = newAuthTokens.AccessToken.Token;

            return Ok(accessToken);
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
