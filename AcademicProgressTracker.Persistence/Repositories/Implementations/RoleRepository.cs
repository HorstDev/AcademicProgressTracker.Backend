using AcademicProgressTracker.Domain;
using AcademicProgressTracker.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicProgressTracker.Persistence.Repositories.Implementations
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AcademicProgressDataContext _db;

        public RoleRepository(AcademicProgressDataContext db)
        {
            _db = db;
        }

        public async Task<Role?> GetByName(string roleName)
        {
            return await _db.Roles.SingleOrDefaultAsync(x => x.Name == roleName);
        }
    }
}
