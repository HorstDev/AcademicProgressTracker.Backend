using System.ComponentModel.DataAnnotations;

namespace AcademicProgressTracker.Application.Common.ViewModels.LabWork
{
    public class LabWorkStatusViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal CurrentScore { get; set; }
        public decimal MaximumScore { get; set; }
        public bool IsCompleted { get; set; }
    }
}
