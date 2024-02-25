using AcademicProgressTracker.Application.Common.ViewModels.LabWork;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabWorkController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public LabWorkController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet("students-with-labs/{subjectId}")]
        public async Task<ActionResult<IEnumerable<LabWorksWithWtudentViewModel>>> GetStudentsWithLabWorksBySubjectId(Guid subjectId)
        {
            var labWorkStatuses = await _dataContext.LabWorkStatuses
                .Where(x => x.LabWork!.SubjectId == subjectId)
                .Include(x => x.LabWork)
                .Include(x => x.User)
                .ToListAsync();

            var studentsWithLabWorks = labWorkStatuses
                .GroupBy(x => x.User)
                .Select(group => new LabWorksWithWtudentViewModel
                {
                    Name = group.Key!.Email,
                    LabWorks = group.Select(lws => new LabWorkStatusViewModel
                    {
                        Id = lws.Id,
                        Number = lws.LabWork!.Number,
                        IsCompleted = lws.IsCompleted,
                        MaximumScore = lws.LabWork.MaximumScore,
                        CurrentScore = lws.CurrentScore
                    }).OrderBy(x => x.Number).ToList()
                });

            return Ok(studentsWithLabWorks);
        }

        [HttpPut("lab-completed")]
        public async Task<ActionResult<LabWorkStatusViewModel>> MakeLabWorkDone(LabWorkStatusViewModel labWorkStatusVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Передаем в сервис labWorkStatusVm

            var labWorkStatus = await _dataContext.LabWorkStatuses
                .Include(x => x.LabWork)
                .Include(x => x.User)
                .SingleAsync(x => x.Id == labWorkStatusVm.Id);

            if (labWorkStatusVm.CurrentScore <= labWorkStatus.LabWork!.MaximumScore && labWorkStatusVm.CurrentScore > 0)
            {
                labWorkStatus.CurrentScore = labWorkStatusVm.CurrentScore;
                labWorkStatus.IsCompleted = labWorkStatusVm.IsCompleted = true;
            }
            else
                return BadRequest();

            _dataContext.LabWorkStatuses.Update(labWorkStatus);
            await _dataContext.SaveChangesAsync();

            return Ok(labWorkStatusVm);
        }

        [HttpPut("make-lab-not-completed/{labWorkStatusId}")]
        public async Task<ActionResult<LabWorkStatusViewModel>> MakeLabNotDone(Guid labWorkStatusId)
        {
            var labWorkStatus = await _dataContext.LabWorkStatuses
                .Include(x => x.LabWork)
                .Include(x => x.User)
                .SingleAsync(x => x.Id == labWorkStatusId);

            labWorkStatus.IsCompleted = false;
            labWorkStatus.CurrentScore = 0;

            _dataContext.LabWorkStatuses.Update(labWorkStatus);
            await _dataContext.SaveChangesAsync();

            return Ok(labWorkStatus);
        }
    }
}
