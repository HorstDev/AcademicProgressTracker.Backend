using AcademicProgressTracker.Domain;

namespace AcademicProgressTracker.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User entity);
        Task<List<User>?> GetAllAsync();
        Task DeleteAsync(User entity);
        Task<User> UpdateAsync(User entity);
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string name);
        Task<User?> GetByRefreshTokenAsync(string? refreshToken);
    }
}
