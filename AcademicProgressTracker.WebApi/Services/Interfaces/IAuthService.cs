using AcademicProgressTracker.Domain;
using AcademicProgressTracker.WebApi.Models;

namespace AcademicProgressTracker.WebApi.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserDTO user);
        Task<AuthTokensDTO> Login(UserDTO user);
        Task<AuthTokensDTO> UpdateRefreshToken(string? oldRefreshToken);
    }
}
