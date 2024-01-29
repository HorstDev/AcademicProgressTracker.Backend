namespace AcademicProgressTracker.Domain.Entities
{
    public abstract class Person
    {
        public Guid? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }

    public class Student : Person
    {
        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
    }

    public class Teacher : Person
    {
        public List<Group> Groups { get; set; } = new();
    }

    public class Administrator : Person
    {

    }
}
