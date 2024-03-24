using AcademicProgressTracker.Application.Common.ViewModels.Lesson;

namespace AcademicProgressTracker.Application.Common.ViewModels.LabWork
{
    public class LabWorkViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal Score { get; set; }
        public List<LabLessonViewModel> Lessons { get; set; } = new();
    }
}
