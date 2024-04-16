using AcademicProgressTracker.Application.Common.ViewModels.User;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public UserController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("{substringName}")]
        public async Task<IEnumerable<UserViewModel>> GetBySubstring(string substringName)
        {
            var users = await _dataContext.Users
                .Where(user => user.Email.Contains(substringName) || user.Profiles.Any(profile => profile.Name.Contains(substringName))) 
                .Include(user => user.Profiles)
                .Include(user => user.Roles)
                .ToListAsync();

            var usersVm = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Profiles = user.Profiles.Select(profile => new UserProfileViewModel
                {
                    Id = profile.Id,
                    Name = profile.Name,
                }).ToList(),
                Roles = user.Roles.Select(role => new UserRoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                }).ToList(),
            });

            return usersVm;
        }
    }
}
