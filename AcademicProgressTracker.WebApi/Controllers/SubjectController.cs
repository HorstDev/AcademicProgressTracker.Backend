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
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public SubjectController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("taught-subjects"), Authorize(Roles = "Teacher")]
        public async Task<IEnumerable<SubjectViewModel>> GetTaughtSubjects()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Сначала находим все группы, принадлежащие каждому преподавателю
            // Затем у каждой такой группы находим предметы с максимальным семестром (текущим) и принадлежащие преподавателю и формируем viewModel
            return await _dataContext.Groups
                .Where(group => group.Subjects.Any(subject => subject.Users.Any(user => user.Id == teacherId)))
                .SelectMany(group => group.Subjects
                    .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester) && subject.Users.Any(user => user.Id == teacherId))
                    .Select(subject => new SubjectViewModel
                    {
                        Id = subject.Id,
                        Name = subject.Name,
                        GroupName = group.Name
                    }))
                .OrderBy(subjectViewModel => subjectViewModel.GroupName)
                .ToListAsync();
        }
    }
}
