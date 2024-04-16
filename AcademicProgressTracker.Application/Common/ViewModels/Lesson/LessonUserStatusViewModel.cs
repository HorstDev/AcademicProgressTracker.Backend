namespace AcademicProgressTracker.Application.Common.ViewModels.Lesson
{
    public class LessonUserStatusViewModel
    {
        public Guid Id { get; set; }
        public bool IsVisited { get; set; }
        public decimal Score { get; set; }
    }
}
