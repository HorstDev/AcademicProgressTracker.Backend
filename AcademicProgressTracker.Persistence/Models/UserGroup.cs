using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Persistence.Models
{
    public class UserGroup
    {
        public Guid Id {  get; set; }

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
