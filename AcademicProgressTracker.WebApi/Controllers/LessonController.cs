﻿using AcademicProgressTracker.Application.Common.ViewModels.Lesson;
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
    public class LessonController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public LessonController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Получение списка лабораторных работ у предмета
        /// </summary>
        /// <param name="subjectId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает занятия, которые идут в данный момент по расписанию у преподавателя
        /// </summary>
        /// <returns>Занятия, которые идут в данный момент по расписанию у преподавателя</returns>
        [HttpGet("current-lessons"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetCurrentLessons()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Извлекаем текущие занятия (которые идут в данный момент) вместе с предметами и группами
            var currentLessons = await _dataContext.Lessons
                .Where(lesson => (lesson.Start < DateTime.Now && lesson.End > DateTime.Now || lesson.RealStart < DateTime.Now && lesson.RealEnd > DateTime.Now) 
                    && lesson.Subject!.Users.Any(x => x.Id == teacherId))
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
                    RealStart = lesson.RealStart,
                    RealEnd = lesson.RealEnd,
                    IsStarted = lesson.IsStarted,
                    Type = lesson.GetType().Name,
                });
            }
            return Ok(lessonsViewModels);
        }

        /// <summary>
        /// Возвращает статусы + занятия, которые проводятся преподавателем в данный момент
        /// </summary>
        /// <returns>Статусы + занятия, которые проводятся преподавателем в данный момент</returns>
        [HttpGet("lessons-in-progress-user-statuses"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonUserStatusesViewModel>>> GetCurrentLessonsWithUserStatuses()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            // Извлекаем занятия, которые начаты преподавателем и проводятся в данный момент
            var lessons = await _dataContext.Lessons
                .Where(lesson => lesson.RealStart <= DateTime.Now && lesson.RealEnd >= DateTime.Now 
                    && lesson.Subject!.Users.Any(x => x.Id == teacherId))
                .Include(lesson => lesson.Subject)
                .Include(lesson => lesson.Subject!.Group)
                .Include(lesson => lesson.UserStatuses)
                    .ThenInclude(userStatus => userStatus.User)
                        .ThenInclude(user => user!.Profiles)
                .ToListAsync();

            var listOfLessonUserStatusesViewModel = new List<LessonUserStatusesViewModel>();
            foreach(var lesson in lessons)
            {
                // Извлекаем список: студент - статус занятия
                var userStatusesWithUsers = lesson.UserStatuses
                    .Select(userStatus => new LessonUserStatusWithUserViewModel
                    {
                        StudentName = userStatus.User!.Profiles.OfType<StudentProfile>().Select(x => x.Name).First(),
                        LessonUserStatus = new LessonUserStatusViewModel
                        {
                            Id = userStatus.Id,
                            IsVisited = userStatus.IsVisited,
                            Score = userStatus.Score,
                        }
                    })
                    .OrderBy(lessonUserStatusWithUserVm => lessonUserStatusWithUserVm.StudentName)
                    .ToList();

                // Добавляем в результат занятие и соответствующий ему список "студент - статус занятия"
                listOfLessonUserStatusesViewModel.Add(new LessonUserStatusesViewModel
                {
                    Lesson = new LessonViewModel
                    {
                        Id = lesson.Id,
                        SubjectName = lesson.Subject!.Name,
                        GroupName = lesson.Subject.Group!.Name,
                        Start = lesson.Start,
                        End = lesson.End,
                        RealStart = lesson.RealStart,
                        RealEnd = lesson.RealEnd,
                        IsStarted = lesson.IsStarted,
                        Type = lesson.GetType().Name,
                    },

                    LessonUserStatusesWithUsers = userStatusesWithUsers
                });
            }

            return Ok(listOfLessonUserStatusesViewModel);
        }

        /// <summary>
        /// Получение списка занятий преподавателя, которые проводятся в дату
        /// </summary>
        /// <param name="date">Дата</param>
        /// <returns>Список занятий преподавателя, которые проводятся в дату</returns>
        [HttpGet("lessons-in-date/{date}"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> GetLessonsInDate(DateOnly date)
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Извлекаем занятия, которые идут в день day у преподавателя с id == teacherId
            var lessonsInDay = await _dataContext.Lessons
                .Where(lesson => lesson.Start.Day == date.Day && lesson.Start.Month == date.Month && lesson.Start.Year == date.Year
                    && lesson.Subject!.Users.Any(x => x.Id == teacherId))
                .Include(lesson => lesson.Subject)
                    .ThenInclude(subject => subject!.Group)
                .OrderBy(lesson => lesson.Start)
                .ToListAsync();

            // Маппим в viewModels
            var lessonsViewModels = new List<LessonViewModel>();
            foreach (var lesson in lessonsInDay)
            {
                lessonsViewModels.Add(new LessonViewModel
                {
                    Id = lesson.Id,
                    SubjectName = lesson.Subject!.Name,
                    GroupName = lesson.Subject.Group!.Name,
                    Start = lesson.Start,
                    End = lesson.End,
                    RealStart = lesson.RealStart,
                    RealEnd = lesson.RealEnd,
                    IsStarted = lesson.IsStarted,
                    Type = lesson.GetType().Name,
                });
            }
            return Ok(lessonsViewModels);
        }

        /// <summary>
        /// Начать занятия
        /// </summary>
        /// <param name="lessonsVm"></param>
        /// <returns>Список начатых занятий</returns>
        [HttpPut("start-lessons"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> StartLessons(IEnumerable<LessonViewModel> lessonsVm)
        {
            var lessonsIds = lessonsVm.Select(x => x.Id).ToList();
            // Извлекаем все занятия
            var lessons = await _dataContext.Lessons
                .Where(lesson => lessonsIds.Contains(lesson.Id))
                .Include(lesson => lesson.Subject)
                    .ThenInclude(subject => subject!.Group)
                .ToListAsync();

            var now = DateTime.Now;
            DateTime start, end;
            // Находим среди занятий то, которое проводится в соответствии с временем, указанным в расписании
            var scheduledLesson = lessons.FirstOrDefault(lesson => lesson.Start < now && now < lesson.End);
            // Если такое занятие нашлось, то для всех занятий устанавливаем время, указанное в расписании, иначе текущее время
            if (scheduledLesson != null)
            {
                start = scheduledLesson.Start;
                end = scheduledLesson.End;
            }
            else
            {
                start = now;
                end = start.AddMinutes(90);
            }

            // У каждого занятия изменяем его данные
            foreach (var lesson in lessons)
            {
                lesson.RealStart = start;
                lesson.RealEnd = end;
                lesson.IsStarted = true;
                
                // Выбираем пользователей студентов для занятия
                var studentUsers = await _dataContext.UserGroup
                    .Where(x => x.Group!.Subjects.Any(subject => subject.Id == lesson.SubjectId) && x.Role!.Name == "Student")
                    .Select(x => x.User)
                    .ToListAsync();

                // Добавляем статус занятия для каждого пользователя
                foreach(var user in studentUsers)
                {
                    lesson.UserStatuses.Add(new LessonUserStatus
                    {
                        User = user,
                        Lesson = lesson,
                        IsVisited = false,
                        Score = 0,
                    });
                }
            }
            await _dataContext.SaveChangesAsync();

            var lessonsViewModel = new List<LessonViewModel>();
            foreach (var lesson in lessons)
            {
                lessonsViewModel.Add(new LessonViewModel
                {
                    Id = lesson.Id,
                    SubjectName = lesson.Subject!.Name,
                    GroupName = lesson.Subject.Group!.Name,
                    Start = lesson.Start,
                    End = lesson.End,
                    RealStart = lesson.RealStart,
                    RealEnd = lesson.RealEnd,
                    IsStarted = lesson.IsStarted,
                    Type = lesson.GetType().Name,
                });
            }

            return lessonsViewModel;
        }

        /// <summary>
        /// Остановить занятия
        /// </summary>
        /// <param name="lessonsVm"></param>
        /// <returns>Список остановленных занятий</returns>
        [HttpPut("stop-lessons"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonViewModel>>> StopLessons(IEnumerable<LessonViewModel> lessonsVm)
        {
            var lessonsIds = lessonsVm.Select(x => x.Id).ToList();
            // Извлекаем все занятия
            var lessons = await _dataContext.Lessons
                .Where(lesson => lessonsIds.Contains(lesson.Id))
                .Include(lesson => lesson.Subject)
                    .ThenInclude(subject => subject!.Group)
                .ToListAsync();

            var now = DateTime.Now;
            foreach(var lesson in lessons)
                lesson.RealEnd = now;
            await _dataContext.SaveChangesAsync();

            var lessonsViewModel = new List<LessonViewModel>();
            foreach (var lesson in lessons)
            {
                lessonsViewModel.Add(new LessonViewModel
                {
                    Id = lesson.Id,
                    SubjectName = lesson.Subject!.Name,
                    GroupName = lesson.Subject.Group!.Name,
                    Start = lesson.Start,
                    End = lesson.End,
                    RealStart = lesson.RealStart,
                    RealEnd = lesson.RealEnd,
                    IsStarted = lesson.IsStarted,
                    Type = lesson.GetType().Name,
                });
            }

            return lessonsViewModel;
        }

        /// <summary>
        /// Обновление статусов занятий
        /// </summary>
        /// <param name="lessonsUserStatusesVm"></param>
        /// <returns></returns>
        [HttpPut("update-lesson-statuses"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<IEnumerable<LessonUserStatusViewModel>>> UpdateLessonUserStatuses(IEnumerable<LessonUserStatusViewModel> lessonsUserStatusesVm)
        {
            var lessonStatusesIds = lessonsUserStatusesVm.Select(x => x.Id).ToList();
            // Извлекаем все занятия
            var lessonStatuses = await _dataContext.LessonUserStatuses
                .Where(lesson => lessonStatusesIds.Contains(lesson.Id))
                .ToListAsync();

            // Для каждого занятия обновляем статус посещения и балл и сохраняем изменения
            foreach(var lessonStatus in lessonStatuses)
            {
                var lessonStatusVm = lessonsUserStatusesVm.Single(x => x.Id == lessonStatus.Id);
                lessonStatus.IsVisited = lessonStatusVm.IsVisited;
                lessonStatus.Score = lessonStatusVm.Score;
            }
            await _dataContext.SaveChangesAsync();

            // Преобразуем в viewModel
            var updatedLessonStatusesVm = lessonStatuses.Select(lessonStatus => new LessonUserStatusViewModel
            {
                Id = lessonStatus.Id,
                IsVisited = lessonStatus.IsVisited,
                Score = lessonStatus.Score,
            }).ToList();
            return updatedLessonStatusesVm;
        }

        /// <summary>
        /// Возвращает статус занятия, которое проводится в данный момент у студента
        /// </summary>
        /// <returns>Статус занятия, которое проводится в данный момент у студента</returns>
        [HttpGet("active-lesson-status"), Authorize(Roles = "Student")]
        public async Task<ActionResult<LessonUserStatusViewModel>> GetActiveLessonStatus()
        {
            var studentId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var activeLessonStatus = await _dataContext.LessonUserStatuses
                .Where(lessonStatus => lessonStatus.UserId == studentId
                && lessonStatus.Lesson!.RealStart < DateTime.Now && DateTime.Now < lessonStatus.Lesson.RealEnd)
                .SingleOrDefaultAsync();

            if (activeLessonStatus == null)
                throw new BadHttpRequestException("В данный момент занятия не проводятся!");

            return new LessonUserStatusViewModel
            {
                Id = activeLessonStatus.Id,
                IsVisited = activeLessonStatus.IsVisited,
                Score = activeLessonStatus.Score,
            };
        }

        [HttpPut("check-lesson-status-visited/{lessonStatusId}"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<LessonUserStatusViewModel>> CheckLessonStatusVisited(Guid lessonStatusId)
        {
            var lessonStatus = await _dataContext.LessonUserStatuses
                .Include(x => x.Lesson)
                .SingleAsync(x => x.Id == lessonStatusId);

            // Проверяем ситуацию, когда студент сохранил картинку qr кода и решил отсканировать ее позже
            if (lessonStatus.Lesson!.RealEnd < DateTime.Now)
                throw new BadHttpRequestException("Занятие уже окончено! Отметиться нельзя");

            if (lessonStatus.IsVisited)
                throw new BadHttpRequestException("Вы уже отмечены, как посетивший занятие!");

            // Обновляем статус занятия на посещенный
            lessonStatus.IsVisited = true;
            await _dataContext.SaveChangesAsync();

            return new LessonUserStatusViewModel
            {
                Id = lessonStatus.Id,
                IsVisited = lessonStatus.IsVisited,
                Score = lessonStatus.Score,
            };
        }
    }
}
