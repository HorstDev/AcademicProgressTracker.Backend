using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Application.Common.Interfaces.Repositories
{
    public interface IGroupRepository
    {
        Task DeleteAsync(Group group);
    }
}
