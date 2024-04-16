namespace AcademicProgressTracker.Application.Common.ViewModels.Group
{
    public class GroupViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateTimeOfUpdateDependenciesFromServer { get; set; }
    }
}
