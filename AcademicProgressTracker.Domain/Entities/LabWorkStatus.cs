namespace AcademicProgressTracker.Domain.Entities
{
    public class LabWorkStatus
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; } = false;

        public Guid LabWorkId { get; set; }
        public LabWork? LabWork { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
