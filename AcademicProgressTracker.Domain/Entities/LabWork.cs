namespace AcademicProgressTracker.Domain.Entities
{
    public class LabWork
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public float MaximumScore { get; set; }

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}
