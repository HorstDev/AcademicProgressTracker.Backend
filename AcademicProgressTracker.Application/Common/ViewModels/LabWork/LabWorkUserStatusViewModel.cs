using System.ComponentModel.DataAnnotations;

namespace AcademicProgressTracker.Application.Common.ViewModels.LabWork
{
    public class LabWorkUserStatusViewModel
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal Score { get; set; }
        public bool IsDone { get; set; }
    }
}
