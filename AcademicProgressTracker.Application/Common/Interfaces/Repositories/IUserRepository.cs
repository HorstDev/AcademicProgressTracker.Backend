using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> CreateStudentUserAsync(Student student);
        Task<List<User>?> GetAllAsync();
        Task DeleteAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string name);
        Task<User?> GetByRefreshTokenAsync(string? refreshToken);
    }
}
