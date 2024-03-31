namespace AcademicProgressTracker.Application.Common.ViewModels.Lesson
{
    public class LessonUserStatusesViewModel
    {
        public LessonViewModel Lesson { get; set; } = new();
        public List<LessonUserStatusWithUserViewModel> LessonUserStatusesWithUsers { get; set; } = new();
    }
}
