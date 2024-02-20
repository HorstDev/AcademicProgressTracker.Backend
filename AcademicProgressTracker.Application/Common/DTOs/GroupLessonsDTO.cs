namespace AcademicProgressTracker.Application.Common.DTOs
{
    public class GroupWithLessonsDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<Lesson> Lessons { get; set; } = new();
    }

    public class Lesson
    {
        public List<Entry> Entries { get; set; } = new();
    }

    public class Entry
    {
        public string Teacher { get; set; } = string.Empty;
        public string Discipline { get; set; } = string.Empty;
    }
}
