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

        public async Task<User> CreateStudentUserAsync(Student student)
        {
            if (student.User == null)
                throw new ApplicationException("Сбой при регистрации. User is null");

            student.Id = Guid.NewGuid();
            student.User.Id = Guid.NewGuid();
            student.UserId = student.User.Id;
            await _db.Users.AddAsync(student.User);
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();

            return student.User;
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
