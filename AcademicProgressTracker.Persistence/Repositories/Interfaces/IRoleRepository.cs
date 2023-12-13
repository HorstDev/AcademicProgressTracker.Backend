using AcademicProgressTracker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicProgressTracker.Persistence.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByName(string name);
    }
}
