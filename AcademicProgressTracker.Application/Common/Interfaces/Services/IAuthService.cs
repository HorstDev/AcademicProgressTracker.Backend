using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.ViewModels.Auth;
using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        Task<User> RegisterStudentUserAsync(RegisterStudentViewModel user);
        Task<AuthTokensDto> LoginAsync(UserDto user);
        Task<AuthTokensDto> UpdateRefreshTokenAsync(string? oldRefreshToken);
    }
}
