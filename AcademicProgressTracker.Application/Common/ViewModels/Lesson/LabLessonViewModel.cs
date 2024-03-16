namespace AcademicProgressTracker.Application.Common.ViewModels.Lesson
{
    public class LabLessonViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public bool HasLabWork { get; set; } = false;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public bool IsStarted { get; set; }
    }
}
