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

        [HttpPost("create-many/{subjectId}")]
        public async Task<ActionResult> AddRange(Guid subjectId, IEnumerable<AddLabWorkViewModel> labWorksVm)
        {
            // Выбираем все лабораторные занятия и упорядочиваем по номеру, чтобы было удобно проходить по ним
            var labLessons = await _dataContext.LabLessons
                .Where(x => x.SubjectId == subjectId)
                .OrderBy(x => x.Number)
                .ToListAsync();
            if (labLessons.Count != labWorksVm.Sum(x => x.LessonCount))
                return BadRequest("Ошибка! Количество занятий, выделенных на ЛР, не совпадает с количеством занятий в расписании!");

            var labWorksToDatabase = new List<LabWork>();
            int currentLesson = 0;
            // Проходимся по всем лабораторным
            foreach (var labWork in labWorksVm)
            {
                var labWorkToDatabase = new LabWork { Number = labWork.Number, Score = labWork.Score };
                // Проходимся по количеству занятий у лабораторной и добавляем ей соответствующие занятия
                for (int i = 0; i < labWork.LessonCount; i++)
                {
                    labWorkToDatabase.Lessons.Add(labLessons[currentLesson++]);
                }
                labWorksToDatabase.Add(labWorkToDatabase);
            }

            await _dataContext.LabWorks.AddRangeAsync(labWorksToDatabase);
            await _dataContext.SaveChangesAsync();

            return Created();
        }

        //[HttpGet("students-with-labs/{subjectId}")]
        //public async Task<ActionResult<IEnumerable<LabWorksWithWtudentViewModel>>> GetStudentsWithLabWorksBySubjectId(Guid subjectId)
        //{
        //    var labWorkStatuses = await _dataContext.LabWorkUserStatuses
        //        .Where(x => x.LabWork!.SubjectId == subjectId)
        //        .Include(x => x.LabWork)
        //        .Include(x => x.User)
        //        .ToListAsync();

        //    var studentsWithLabWorks = labWorkStatuses
        //        .GroupBy(x => x.User)
        //        .Select(group => new LabWorksWithWtudentViewModel
        //        {
        //            Name = group.Key!.Email,
        //            LabWorks = group.Select(lws => new LabWorkStatusViewModel
        //            {
        //                Id = lws.Id,
        //                Number = lws.LabWork!.Number,
        //                IsCompleted = lws.IsCompleted,
        //                MaximumScore = lws.LabWork.MaximumScore,
        //                CurrentScore = lws.CurrentScore
        //            }).OrderBy(x => x.Number).ToList()
        //        });

        //    return Ok(studentsWithLabWorks);
        //}

        //[HttpPut("lab-completed")]
        //public async Task<ActionResult<LabWorkStatusViewModel>> MakeLabWorkDone(LabWorkStatusViewModel labWorkStatusVm)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    // Передаем в сервис labWorkStatusVm

        //    var labWorkStatus = await _dataContext.LabWorkStatuses
        //        .Include(x => x.LabWork)
        //        .Include(x => x.User)
        //        .SingleAsync(x => x.Id == labWorkStatusVm.Id);

        //    if (labWorkStatusVm.CurrentScore <= labWorkStatus.LabWork!.MaximumScore && labWorkStatusVm.CurrentScore > 0)
        //    {
        //        labWorkStatus.CurrentScore = labWorkStatusVm.CurrentScore;
        //        labWorkStatus.IsCompleted = labWorkStatusVm.IsCompleted = true;
        //    }
        //    else
        //        return BadRequest();

        //    _dataContext.LabWorkStatuses.Update(labWorkStatus);
        //    await _dataContext.SaveChangesAsync();

        //    return Ok(labWorkStatusVm);
        //}

        //[HttpPut("make-lab-not-completed/{labWorkStatusId}")]
        //public async Task<ActionResult<LabWorkStatusViewModel>> MakeLabNotDone(Guid labWorkStatusId)
        //{
        //    var labWorkStatus = await _dataContext.LabWorkStatuses
        //        .Include(x => x.LabWork)
        //        .Include(x => x.User)
        //        .SingleAsync(x => x.Id == labWorkStatusId);

        //    labWorkStatus.IsCompleted = false;
        //    labWorkStatus.CurrentScore = 0;

        //    _dataContext.LabWorkStatuses.Update(labWorkStatus);
        //    await _dataContext.SaveChangesAsync();

        //    return Ok(labWorkStatus);
        //}
    }
}
