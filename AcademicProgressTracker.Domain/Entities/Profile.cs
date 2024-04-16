namespace AcademicProgressTracker.Domain.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class TeacherProfile : Profile
    {
        
    }

    public class StudentProfile : Profile
    {

    }
}
