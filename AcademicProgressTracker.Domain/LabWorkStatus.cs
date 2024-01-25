namespace AcademicProgressTracker.Domain
{
    public class LabWorkStatus
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; } = false;
        
        public Guid LabWorkId { get; set; }
        public LabWork? LabWork { get; set; }
        public Guid StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
