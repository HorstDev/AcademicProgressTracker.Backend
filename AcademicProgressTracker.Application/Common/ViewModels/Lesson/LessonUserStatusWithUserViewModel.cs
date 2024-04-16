namespace AcademicProgressTracker.Application.Common.ViewModels.Lesson
{
    public class LessonUserStatusWithUserViewModel
    {
        public string StudentName { get; set; } = string.Empty;
        public LessonUserStatusViewModel LessonUserStatus { get; set; } = new();
    }
}
