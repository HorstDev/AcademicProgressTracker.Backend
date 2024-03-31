namespace AcademicProgressTracker.Domain.Entities
{
    public class LessonUserStatus
    {
        public Guid Id { get; set; }
        public bool IsVisited { get; set; } = false;
        public decimal Score { get; set; } = 0;
        
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid LessonId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}
