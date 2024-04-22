using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.Persistence.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly AcademicProgressDataContext _db;

        public GroupRepository(AcademicProgressDataContext db)
        {
            _db = db;
        }

        public async Task DeleteAsync(Group group)
        {
            // Выбираем всех студентов, которых будем удалять вместе с группой
            var usersInGroup = await _db.UserGroup
                .Where(x => x.Group!.Id == group.Id && x.Role!.Name == "Student")
                .Select(x => x.User)
                .ToListAsync();

            foreach (var user in usersInGroup)
                _db.Users.Remove(user!);

            _db.Groups.Remove(group);
            await _db.SaveChangesAsync();
        }
    }
}
