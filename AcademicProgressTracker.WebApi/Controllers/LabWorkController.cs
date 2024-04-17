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

        /// <summary>
        /// Добавление лабораторной работы
        /// </summary>
        /// <param name="labWorkVm"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<LabWork>> Post(AddLabWorkViewModel labWorkVm)
        {
            // Выбираем лабораторные занятия, которые были указаны как привязанные к лабораторной работе
            var labLessons = await _dataContext.LabLessons.Where(labLesson => labWorkVm.LabLessonsIds.Contains(labLesson.Id))
                .Include(x => x.Subject)
                .ToListAsync();

            // Если лабораторных занятий, указанных на клиенте не нашлось
            if (!labLessons.Any())
                return BadRequest("Лабораторные занятия не указаны!");

            // Выбираем id группы, в которой происходит добавление ЛР
            var groupId = labLessons[0].Subject!.GroupId;
            // Выбираем всех студентов, которые состоят в группе, для которой добавляется лабораторная работа
            var studentIdsInGroup = await _dataContext.UserGroup.Where(x => x.GroupId == groupId && x.Role!.Name == "Student")
                .Select(x => x.UserId)
                .ToListAsync();

            // Для каждого пользователя в группе добавляем статус выполнения лабораторной работы
            var userStatuses = new List<LabWorkUserStatus>();
            foreach(var studentUserId in studentIdsInGroup)
            {
                userStatuses.Add(new LabWorkUserStatus
                {
                    UserId = studentUserId,
                    IsDone = false
                });
            }

            var labWork = new LabWork
            {
                Number = labWorkVm.Number,
                Score = labWorkVm.Score,
                UserStatuses = userStatuses,
                Lessons = labLessons
            };

            await _dataContext.LabWorks.AddAsync(labWork);
            await _dataContext.SaveChangesAsync();

            return Created();
        }

        /// <summary>
        /// Удаление лабораторной работы
        /// </summary>
        /// <param name="labWorkId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение списка лабораторных работ у предмета
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Получение списка студентов с статусами лабораторных работ
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
        [HttpGet("{subjectId}/students-labWork-statuses")]
        public async Task<ActionResult<IEnumerable<LabWorksWithWtudentViewModel>>> GetStudentsWithLabWorkStatuses(Guid subjectId)
        {
            // Получаем статусы лабораторных работ вместе с пользователями
            var labWorkUserStatuses = await _dataContext.LabWorkUserStatuses
                .Where(labWorkUserStatus => labWorkUserStatus.LabWork!.Lessons.Any(lesson => lesson.SubjectId == subjectId))
                .Include(labWorkUserStatus => labWorkUserStatus.User)
                    .ThenInclude(user => user!.Profiles)
                .Include(labWorkUserStatus => labWorkUserStatus.LabWork)
                .ToListAsync();

            var studentsWithLabWorkStatuses = labWorkUserStatuses
                .GroupBy(x => x.User)
                .Select(group => new LabWorksWithWtudentViewModel
                {
                    // Получаем имя студента, извлекая его из профиля студента
                    Name = group.Key!.Profiles.OfType<StudentProfile>().Select(x => x.Name).First(),

                    // И статусы лаб
                    LabWorkUserStatuses = group.Select(lws => new LabWorkUserStatusViewModel
                    {
                        Id = lws.Id,
                        Number = lws.LabWork!.Number,
                        IsDone = lws.IsDone,
                        Score = lws.LabWork.Score,
                    }).OrderBy(x => x.Number).ToList()
                }).OrderBy(lwWithStudentsVm => lwWithStudentsVm.Name);

            return Ok(studentsWithLabWorkStatuses);
        }

        /// <summary>
        /// Изменение состояние у статуса лабораторной работы
        /// </summary>
        /// <param name="labWorkUserStatusViewModel"></param>
        /// <returns></returns>
        [HttpPut("lab-work-status-change-state")]
        public async Task<ActionResult<LabWorkUserStatusViewModel>> ChangeStateOfLabWorkStatus(LabWorkUserStatusViewModel labWorkUserStatusViewModel)
        {
            var labWorkUserStatus = await _dataContext.LabWorkUserStatuses
                .SingleAsync(lwStatus => lwStatus.Id == labWorkUserStatusViewModel.Id);

            // Меняем статус у ЛР
            labWorkUserStatus.IsDone = !labWorkUserStatus.IsDone;
            _dataContext.Update(labWorkUserStatus);
            await _dataContext.SaveChangesAsync();

            // Меняем статус у viewModel
            labWorkUserStatusViewModel.IsDone = labWorkUserStatus.IsDone;
            return Ok(labWorkUserStatusViewModel);
        }
    }
}
