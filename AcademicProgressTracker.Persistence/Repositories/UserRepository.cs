using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AcademicProgressDataContext _db;

        public UserRepository(AcademicProgressDataContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(User entity)
        {
            if (entity.Id == null)
            {
                entity.Id = Guid.NewGuid();
                await _db.Users.AddAsync(entity);
                await _db.SaveChangesAsync();
            }
            else
            {
                await UpdateAsync(entity);
            }
        }

        public async Task<List<User>?> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<User> UpdateAsync(User entity)
        {
            _db.Users.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _db.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _db.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User?> GetByRefreshTokenAsync(string? refreshToken)
        {
            return await _db.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }
    }
}
