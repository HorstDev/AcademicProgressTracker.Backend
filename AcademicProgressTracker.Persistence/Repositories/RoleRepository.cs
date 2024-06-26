﻿using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AcademicProgressDataContext _db;

        public RoleRepository(AcademicProgressDataContext db)
        {
            _db = db;
        }

        public async Task<Role?> GetByNameAsync(string roleName)
        {
            return await _db.Roles.SingleAsync(x => x.Name == roleName);
        }
    }
}
