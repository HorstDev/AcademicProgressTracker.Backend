namespace AcademicProgressTracker.Domain.Entities
{
    public class LabWork
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal Score { get; set; }

        public ICollection<LabLesson> Lessons { get; set; } = new List<LabLesson>();
        public ICollection<LabWorkUserStatus> UserStatuses { get; set; } = new List<LabWorkUserStatus>();
    }
}
