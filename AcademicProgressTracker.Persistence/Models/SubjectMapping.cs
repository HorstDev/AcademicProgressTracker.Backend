namespace AcademicProgressTracker.Persistence.Models
{
    /// <summary>
    /// Отвечает за маппинг дисциплин
    /// SubjectNameApiTable - название дисциплины, которое предоставляет apitable на сервере АГТУ
    /// SubjectNameCurriculum - название дисциплины в учебном плане (если == null, значит дисциплина отсутствует в учебном плане)
    /// </summary>
    public class SubjectMapping
    {
        public Guid Id { get; set; }
        public string SubjectNameApiTable { get; set; } = string.Empty;
        public string? SubjectNameCurriculum { get; set; }
    }
}
