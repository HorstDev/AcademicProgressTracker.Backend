namespace AcademicProgressTracker.Domain
{
    public abstract class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public class Student : Person
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid GroupId { get; set; }
        public Group? Group { get; set; }
    }

    public class Teacher : Person
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public List<Group> Groups { get; set; } = new();
    }

    public class Administrator : Person
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
