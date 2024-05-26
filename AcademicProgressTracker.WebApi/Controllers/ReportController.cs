using AcademicProgressTracker.Application.Common.ViewModels.Report;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public ReportController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Получение отчета по группе
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupReportViewModel>> GetReportForGroup(Guid groupId)
        {
            //// Студенты в группе
            //var studentsInGroup = await _dataContext.UserGroup.Where(x => x.GroupId == groupId)
            //    .Select(x => x.User!)
            //        .Include(user => user.Profiles.OfType<StudentProfile>())
            //    .ToListAsync();

            // Выбираем всех студентов у группы
            var studentProfiles = await _dataContext.UserGroup
                .Where(x => x.GroupId == groupId && x.Role!.Name == "Student")
                .Select(x => x.User!.Profiles.OfType<StudentProfile>().First())
                .OrderBy(profile => profile.Name)
                .ToListAsync();

            // Дисциплины в группе
            var subjects = await _dataContext.Groups
            .SelectMany(group => group.Subjects
                .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester) && subject.GroupId == groupId))
                .ToListAsync();

            var subjectsWithLabWorks = new Dictionary<Subject, List<LabWork>>();
            var subjectsWithLessonUserStatuses = new Dictionary<Subject, List<LessonUserStatus>>();

            foreach (var subject in subjects)
            {
                // Извлекаем лабораторные работы для дисциплины
                var labWorksForSubject = await _dataContext.LabWorks
                    .Where(labWork => labWork.Lessons.Any(lesson => lesson.SubjectId == subject.Id))
                    .Include(labWork => labWork.UserStatuses)
                    .ToListAsync();

                // Статусы занятий для дисциплины
                var lessonUserStatuses = await _dataContext.LessonUserStatuses
                    .Where(lessonUserStatus => lessonUserStatus.Lesson!.SubjectId == subject.Id)
                    .Include(lessonUserStatus => lessonUserStatus.Lesson)
                    .ToListAsync();

                subjectsWithLabWorks.Add(subject, labWorksForSubject);
                subjectsWithLessonUserStatuses.Add(subject, lessonUserStatuses);
            }

            //// Извлекаем лабораторные работы вместе с статусами
            //var labWorks = await _dataContext.LabWorks
            //    .Where(labWork => labWork.Lessons.Any(lesson => subjects.Any(subject => subject.Id == lesson.SubjectId)))
            //    .Include(labWork => labWork.UserStatuses)
            //    .ToListAsync();

            //// Все статусы занятий
            //var lessonUserStatuses = await _dataContext.LessonUserStatuses
            //    .Where(lessonUserStatus => subjects.Any(subject => subject.Id == lessonUserStatus.Lesson!.SubjectId))
            //    .ToListAsync();

            var report = new GroupReportViewModel();
            // Добавляем в отчет дисциплины, проводимые у группы
            foreach (var subject in subjects)
            {
                report.AllSubjects.Add(subject.Name);
            }
            // Пробегаемся по каждому студенту
            foreach (var studentProfile in studentProfiles)
            {
                // Устанавливаем имя студента
                var studentReportVm = new StudentReportViewModel();
                studentReportVm.StudentName = studentProfile.Name;
                // Пробегаемся по каждой дисциплине
                foreach (var subject in subjects)
                {
                    // Выбираем баллы для дисциплины (лабы)
                    decimal scoreForStudentOnSubjectOnLabWorks = subjectsWithLabWorks[subject]
                        .Where(labWork => labWork.UserStatuses.Any(status => status.IsDone && status.UserId == studentProfile.UserId))
                        .Sum(labWork => labWork.Score);

                    // Выбираем баллы для дисциплины (занятия)
                    decimal scoreForStudentOnSubjectOnLessons = subjectsWithLessonUserStatuses[subject]
                        .Where(lessonStatus => lessonStatus.UserId == studentProfile.UserId)
                        .Sum(lessonStatus => lessonStatus.Score);

                    // Устанавливаем все баллы
                    decimal fullScore = scoreForStudentOnSubjectOnLessons + scoreForStudentOnSubjectOnLabWorks;

                    // Считаем количество пропусков для дисциплины
                    int notVisitedLessonsCount = subjectsWithLessonUserStatuses[subject]
                        .Where(lessonStatus => !lessonStatus.IsVisited && lessonStatus.UserId == studentProfile.UserId)
                        .Select(lessonStatus => lessonStatus.LessonId)
                        .Distinct()
                        .Count();

                    int startedLessonsCount = subjectsWithLessonUserStatuses[subject]
                        .Select(lessonStatus => lessonStatus.Lesson)
                        .Distinct()
                        .Count(lesson => lesson!.IsStarted);

                    studentReportVm.SubjectsInformationReport.Add(new SubjectInformationReportViewModel
                    {
                        SubjectName = subject.Name,
                        Score = fullScore,
                        NotVisitedLessonCount = notVisitedLessonsCount,
                        StartedLessonCount = startedLessonsCount,
                    });
                }

                report.StudentReports.Add(studentReportVm);
            }

            return report;
        }
    }
}
