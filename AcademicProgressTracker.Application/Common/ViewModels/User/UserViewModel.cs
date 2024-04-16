
namespace AcademicProgressTracker.Application.Common.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public List<UserProfileViewModel> Profiles { get; set; } = new();
        public List<UserRoleViewModel> Roles { get; set; } = new();
    }
}
