namespace AcademicProgressTracker.Application.Common.ViewModels.LabWork
{
    public class AddLabWorkViewModel
    {
        public int Number { get; set; }
        public decimal Score { get; set; }
        public List<Guid> LabLessonsIds { get; set; } = new();
    }
}
