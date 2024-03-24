namespace AcademicProgressTracker.Domain.Entities
{
    public class LabWorkUserStatus
    {
        public Guid Id { get; set; }
        public bool IsDone { get; set; } = false;

        public Guid LabWorkId { get; set; }
        public LabWork? LabWork { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
