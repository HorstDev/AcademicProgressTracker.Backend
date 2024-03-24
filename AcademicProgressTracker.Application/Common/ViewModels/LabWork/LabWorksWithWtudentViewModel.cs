namespace AcademicProgressTracker.Application.Common.ViewModels.LabWork
{
    public class LabWorksWithWtudentViewModel
    {
        public string Name { get; set; } = string.Empty;
        public List<LabWorkUserStatusViewModel> LabWorkUserStatuses { get; set; } = new();
    }
}
