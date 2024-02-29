using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.ViewModels.Auth;
using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Регистрация студента
        /// </summary>
        /// <param name="user">viewModel пользователя студент</param>
        /// <returns></returns>
        Task<User> RegisterStudentUserAsync(RegisterStudentViewModel user);
        Task<AuthTokensDto> LoginAsync(UserDto user);
        Task<AuthTokensDto> UpdateRefreshTokenAsync(string? oldRefreshToken);
        /// <summary>
        /// Получение пользователя "преподаватель"
        /// </summary>
        /// <param name="name">Имя преподавателя</param>
        /// <returns></returns>
        Task<User> GetTeacherUserWithRandomLoginAndPasswordAsync(string name);
    }
}
