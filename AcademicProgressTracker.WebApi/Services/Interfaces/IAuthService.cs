using AcademicProgressTracker.Domain;
using AcademicProgressTracker.WebApi.Models;

namespace AcademicProgressTracker.WebApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserDto user);
        Task<AuthTokensDto> Login(UserDto user);
        Task<AuthTokensDto> UpdateRefreshToken(string? oldRefreshToken);
    }
}
