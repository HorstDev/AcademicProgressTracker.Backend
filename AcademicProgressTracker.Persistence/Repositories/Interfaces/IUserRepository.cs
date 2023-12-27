﻿using AcademicProgressTracker.Domain;

namespace AcademicProgressTracker.Persistence.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task Create(User entity);
        Task<List<User>?> GetAll();
        Task Delete(User entity);
        Task<User> Update(User entity);
        Task<User?> GetById(Guid id);
        Task<User?> GetByEmail(string name);
        Task<User?> GetByRefreshToken(string? refreshToken);
    }
}
