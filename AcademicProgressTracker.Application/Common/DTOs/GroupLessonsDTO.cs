namespace AcademicProgressTracker.Application.Common.DTOs
{
    public class GroupWithLessonsDTO
    {
        public string Name { get; set; } = string.Empty;
        public List<LessonDTO> Lessons { get; set; } = new();
    }

    public class LessonDTO
    {
        public List<EntryDTO> Entries { get; set; } = new();
        public int DayId { get; set; }
        public int LessonOrderId { get; set; }
    }

    public class EntryDTO
    {
        public string Teacher { get; set; } = string.Empty;
        public string Discipline { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
