namespace AcademicProgressTracker.Application.Common.ViewModels.Lesson
{
    public class LessonViewModel
    {
        public Guid Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Type { get; set; } = string.Empty;
        public bool IsStarted { get; set; }
    }
}
