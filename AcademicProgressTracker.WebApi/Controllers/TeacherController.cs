using AcademicProgressTracker.Application.Common.ViewModels.Group;
using AcademicProgressTracker.Application.Common.ViewModels.Subject;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Authorize(Roles = "Teacher")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public TeacherController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("myGroups")]
        public async Task<IEnumerable<GroupViewModel>> GetMyGroups()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            // ТУТ ОШИБКА!! НАДО ЕЩЕ ПРОВЕРИТЬ, ЧТОБЫ РОЛЬ БЫЛА TEACHER
            var groups = await _dataContext.UserGroup
                .Where(x => x.UserId == teacherId)
                .Select(x => x.Group)
                .ToListAsync();

            var groupsView = new List<GroupViewModel>();
            foreach (var group in groups)
            {
                if (group is not null)
                {
                    groupsView.Add(new GroupViewModel { Id = group.Id, Name = group.Name, Course = group.Course });
                }
            }

            return groupsView;
        }

        [HttpGet("mySubjects/{groupId}")]
        public async Task<IEnumerable<SubjectViewModel>> GetMySybjectsByGroupId(Guid groupId)
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var subjects = await _dataContext.TeacherSubject
                .Where(x => x.UserId == teacherId)
                .Select(x => x.Subject)
                .Where(x => x!.GroupId == groupId)
                .ToListAsync();

            var subjectsView = new List<SubjectViewModel>();
            foreach (var subject in subjects)
            {
                if (subject is not null)
                {
                    subjectsView.Add(new SubjectViewModel { Id = subject.Id, Name = subject.Name });
                }
            }

            return subjectsView;
        }
    }
}
