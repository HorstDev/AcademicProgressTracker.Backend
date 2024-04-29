using AcademicProgressTracker.Application.Common.ViewModels.User;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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

        /// <summary>
        /// Получение списка пользователей, имя или логин которых содержат подстроку
        /// </summary>
        /// <param name="substringName">Подстрока</param>
        /// <returns>Список пользователей</returns>
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

        [HttpPut("make-user-admin/{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserViewModel>> MakeAdmin(Guid userId)
        {
            var user = await _dataContext.Users
                .Include(user => user.Profiles)
                .Include(user => user.Roles)
                .SingleAsync(user => user.Id == userId);

            var adminRole = await _dataContext.Roles.SingleAsync(role => role.Name == "Admin");
            user.Roles.Add(adminRole);
            await _dataContext.SaveChangesAsync();

            var userVm = new UserViewModel
            {
                Id = userId,
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
            };

            return userVm;
        }

        [HttpPut("make-user-no-admin/{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserViewModel>> MakeNoAdmin(Guid userId)
        {
            var user = await _dataContext.Users
                .Include(user => user.Profiles)
                .Include(user => user.Roles)
                .SingleAsync(user => user.Id == userId);

            var adminRole = await _dataContext.Roles.SingleAsync(role => role.Name == "Admin");
            user.Roles.Remove(adminRole);
            await _dataContext.SaveChangesAsync();

            var userVm = new UserViewModel
            {
                Id = userId,
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
            };

            return userVm;
        }
    }
}
