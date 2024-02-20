using AcademicProgressTracker.Domain.Entities;

namespace AcademicProgressTracker.Persistence.Models
{
    public class TeacherSubject
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
