namespace AcademicProgressTracker.Domain.Entities
{
    public class Subject
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Semester { get; set; }

        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    }
}
