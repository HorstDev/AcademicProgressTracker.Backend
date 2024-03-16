using AcademicProgressTracker.Application.Common.ViewModels.Lesson;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
