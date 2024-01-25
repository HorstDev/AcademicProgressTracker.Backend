using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Domain;

namespace AcademicProgressTracker.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User> Register(UserDto user);
        Task<AuthTokensDto> Login(UserDto user);
        Task<AuthTokensDto> UpdateRefreshToken(string? oldRefreshToken);
    }
}
