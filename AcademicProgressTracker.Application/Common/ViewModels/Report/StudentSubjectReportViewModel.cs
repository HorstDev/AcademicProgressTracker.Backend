using AcademicProgressTracker.Application.Common.ViewModels.LabWork;

namespace AcademicProgressTracker.Application.Common.ViewModels.Report
{
    public class StudentSubjectReportViewModel
    {
        public string SubjectName { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public int NotVisitedLessonCount { get; set; }
        public int StartedLessonCount { get; set; }
        public List<LabWorkUserStatusViewModel> LabWorkUserStatuses { get; set; } = new();
        public int LabWorkNumberShouldDone { get; set; }
        public bool LabWorksShouldExist { get; set; }
    }
}
