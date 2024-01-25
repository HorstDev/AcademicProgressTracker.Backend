using AcademicProgressTracker.Domain;

namespace AcademicProgressTracker.Application.Common.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<Role?> GetByName(string name);
    }
}
