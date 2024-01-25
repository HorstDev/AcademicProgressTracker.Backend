using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(UserDto user);
        Task<AuthTokensDto> LoginAsync(UserDto user);
        Task<AuthTokensDto> UpdateRefreshTokenAsync(string? oldRefreshToken);
    }
}
