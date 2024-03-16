using AcademicProgressTracker.Application.Common.ViewModels.LabWork;
using AcademicProgressTracker.Application.Common.ViewModels.Lesson;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost]
        public async Task<ActionResult<LabWork>> Post(AddLabWorkViewModel labWorkVm)
        {
            // Это сделать в репозитории (установление id)
            Guid createdId = Guid.NewGuid();

            var labWork = new LabWork
            {
                Id = createdId,
                Number = labWorkVm.Number,
                Score = labWorkVm.Score,
                Lessons = await _dataContext.LabLessons.Where(labLesson => labWorkVm.LabLessonsIds.Contains(labLesson.Id)).ToListAsync()
            };

            await _dataContext.LabWorks.AddAsync(labWork);
            await _dataContext.SaveChangesAsync();

            return Created();
        }

        [HttpDelete("{labWorkId}")]
        public async Task<ActionResult<LabWork>> Delete(Guid labWorkId)
        {
            var labWork = await _dataContext.LabWorks
                .Include(labWork => labWork.Lessons)
                .SingleAsync(labWork => labWork.Id == labWorkId);

            // Обнуляем для каждого занятия LabWorkId, чтобы без проблем удалить лабораторную работу
            foreach(var labLesson in labWork.Lessons)
            {
                labLesson.LabWorkId = null;
            }

            // Обновляем у занятий LabWorkId = null
            _dataContext.Lessons.UpdateRange(labWork.Lessons);
            // Удаляем лабораторную работу
            _dataContext.LabWorks.Remove(labWork);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{subjectId}/get-many"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LabWorkViewModel>>> GetRange(Guid subjectId)
        {
            // Получаем лабы определенного предмета
            var labWorks = await _dataContext.LabWorks
                .Where(labWork => labWork.Lessons.Any(lesson => lesson.SubjectId == subjectId))
                .Distinct()
                .Include(labWork => labWork.Lessons)
                .OrderBy(labWork => labWork.Number)
                .ToListAsync();

            // Маппим данные
            var labWorksVm = new List<LabWorkViewModel>();
            foreach (var labWork in labWorks)
            {
                // Извлекаем все занятия для лабы
                var labWorkLessonsVm = labWork.Lessons.Select(lesson => new LabLessonViewModel
                {
                    Id = lesson.Id,
                    Number = lesson.Number,
                    HasLabWork = lesson.LabWorkId != null,
                    Start = lesson.Start,
                    End = lesson.End,
                    IsStarted = lesson.IsStarted,
                })
                    .OrderBy(l => l.Number)
                    .ToList();

                // Добавляем лабы в массив viewModel
                labWorksVm.Add(new LabWorkViewModel
                {
                    Id = labWork.Id,
                    Number = labWork.Number,
                    Score = labWork.Score,
                    Lessons = labWorkLessonsVm
                });
            }

            return labWorksVm;
        }

        //[HttpPost("create-many/{subjectId}"), Authorize(Roles = "Teacher")]
        //public async Task<ActionResult> AddRange(Guid subjectId, IEnumerable<LabWorkViewModel> labWorksVm)
        //{
        //    // Выбираем все лабораторные занятия и упорядочиваем по номеру, чтобы было удобно проходить по ним
        //    var labLessons = await _dataContext.LabLessons
        //        .Where(x => x.SubjectId == subjectId)
        //        .OrderBy(x => x.Number)
        //        .ToListAsync();
        //    if (labLessons.Count != labWorksVm.Sum(x => x.LessonCount))
        //        return BadRequest("Ошибка! Количество занятий, выделенных на ЛР, не совпадает с количеством занятий в расписании!");

        //    var labWorksToDatabase = new List<LabWork>();
        //    int currentLesson = 0;
        //    // Проходимся по всем лабораторным
        //    foreach (var labWork in labWorksVm)
        //    {
        //        var labWorkToDatabase = new LabWork { Number = labWork.Number, Score = labWork.Score };
        //        // Проходимся по количеству занятий у лабораторной и добавляем ей соответствующие занятия
        //        for (int i = 0; i < labWork.LessonCount; i++)
        //        {
        //            labWorkToDatabase.Lessons.Add(labLessons[currentLesson++]);
        //        }
        //        labWorksToDatabase.Add(labWorkToDatabase);
        //    }

        //    await _dataContext.LabWorks.AddRangeAsync(labWorksToDatabase);
        //    await _dataContext.SaveChangesAsync();

        //    return Created();
        //}

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
