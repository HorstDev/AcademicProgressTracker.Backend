namespace AcademicProgressTracker.Application.Common.ViewModels.Report
{
    public class GroupReportViewModel
    {
        public List<string> AllSubjects { get; set; } = new();
        public List<StudentReportViewModel> StudentReports { get; set; } = new();
    }

    public class StudentReportViewModel
    {
        public string StudentName { get; set; } = string.Empty;
        //public Dictionary<string, decimal> SubjectsWithRaitings { get; set; } = new();
        public List<SubjectInformationReportViewModel> SubjectsInformationReport { get; set; } = new();
    }

    public class SubjectInformationReportViewModel
    {
        public string SubjectName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public int NotVisitedLessonCount { get; set; }
        public int StartedLessonCount { get; set; }
    }
}
