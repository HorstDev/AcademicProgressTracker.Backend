using AcademicProgressTracker.Domain;

namespace AcademicProgressTracker.Persistence.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByName(string name);
    }
}
