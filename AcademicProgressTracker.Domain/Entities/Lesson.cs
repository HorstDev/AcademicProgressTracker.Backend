namespace AcademicProgressTracker.Domain.Entities
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime RealStart { get; set; }
        public DateTime RealEnd { get; set; }
        public bool IsStarted { get; set; } = false;

        public Guid SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public ICollection<LessonUserStatus> UserStatuses { get; set; } = new List<LessonUserStatus>();
    }

    public class LabLesson : Lesson
    {
        public Guid? LabWorkId { get; set; }            // Лаб. занятие может и не относиться к лабораторной работе 
        public LabWork? LabWork { get; set; }
    }

    public class LectureLesson : Lesson
    {

    }

    public class PracticeLesson : Lesson
    {

    }
}
