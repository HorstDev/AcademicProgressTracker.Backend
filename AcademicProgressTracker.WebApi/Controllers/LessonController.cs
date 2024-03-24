using AcademicProgressTracker.Application.Common.ViewModels.Lesson;
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
    public class LessonController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public LessonController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: api/<LessonController>
        [HttpGet("{subjectId}/lab-lessons")]
        public async Task<ActionResult<IEnumerable<LabLessonViewModel>>> GetLabLessons(Guid subjectId)
        {
            return await _dataContext.LabLessons
                .Where(lesson => lesson.SubjectId == subjectId)
                .Select(lesson => new LabLessonViewModel
                {
                    Id = lesson.Id,
                    Number = lesson.Number,
                    HasLabWork = lesson.LabWorkId != null,
                    Start = lesson.Start,
                    End = lesson.End,
                    IsStarted = lesson.IsStarted,
                })
                .OrderBy(labLessonVm => labLessonVm.Number)
                .ToListAsync();
        }

        // Возвращает занятия, которые проводятся в данный момент
        [HttpGet("current-lessons"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetCurrentLessons()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Извлекаем занятия вместе с предметами и группами
            var currentLessons = await _dataContext.Lessons
                .Where(lesson => lesson.Start < DateTime.Now && lesson.End > DateTime.Now && lesson.Subject!.Users.Any(x => x.Id == teacherId))
                .Include(lesson => lesson.Subject)
                    .ThenInclude(subject => subject!.Group)
                .ToListAsync();

            // Маппим в viewModels
            var lessonsViewModels = new List<LessonViewModel>();
            foreach (var lesson in currentLessons)
            {
                lessonsViewModels.Add(new LessonViewModel
                {
                    Id = lesson.Id,
                    SubjectName = lesson.Subject!.Name,
                    GroupName = lesson.Subject.Group!.Name,
                    Start = lesson.Start,
                    End = lesson.End,
                    IsStarted = lesson.IsStarted,
                    Type = lesson.GetType().Name,
                });
            }
            return Ok(lessonsViewModels);
        }
    }
}
