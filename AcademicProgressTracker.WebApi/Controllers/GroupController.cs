using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Curriculum;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;
        private readonly HttpClient _httpClient;

        public GroupController(AcademicProgressDataContext dataContext, HttpClient httpClient)
        {
            _dataContext = dataContext;
            _httpClient = httpClient;
        }

        // Загрузка новой группы
        [HttpPost("{groupName}")]
        public async Task<ActionResult> Create(string groupName, IFormFile excelCurriculum)
        {
            var group = new Group { Name = Uri.UnescapeDataString(groupName) };

            // P.S. НАДО БУДЕТ ДОБАВИТЬ УНИКАЛЬНОСТЬ В КОНФИГУРАЦИИ БД ДЛЯ НАЗВАНИЯ ГРУППЫ ИНАЧЕ ПЛОХО
            if (excelCurriculum != null && excelCurriculum.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    excelCurriculum.CopyTo(memoryStream);
                    var curriculumExcelDocumentBytes = memoryStream.ToArray();
                    group.CurriculumExcelDocument = curriculumExcelDocumentBytes;
                    await _dataContext.AddAsync(group);
                    await _dataContext.SaveChangesAsync();
                    return Ok($"Учебный план {excelCurriculum.FileName} успешно загружен для группы {group.Name}.");
                }
            }

            return BadRequest("Ошибка. Файл пуст.");
        }

        // Загрузка зависимостей для группы (учителя и дисциплины)
        [HttpPost("{groupId}/UploadDependencies")]
        public async Task<ActionResult> UploadTeachersAndSubjects(Guid groupId)
        {
            var group = await _dataContext.Groups.SingleAsync(x => x.Id == groupId);
            var response = await _httpClient.GetAsync($"https://apitable.astu.org/search/get?q={group.Name}&t=group");
            var groupLessonsDTO = await response.Content.ReadFromJsonAsync<GroupWithLessonsDTO>();

            if (groupLessonsDTO != null)
            {
                // Выбираем преподавателей и соответствующие им дисциплины
                var teachersAndSubjects = groupLessonsDTO.Lessons
                    .SelectMany(lesson => lesson.Entries)                       // выбираем все записи (entries) из всех занятий
                    .Select(entry => new { entry.Teacher, entry.Discipline })   // выбираем из всех записей учителя и соответствующую ему дисциплину
                    .Distinct()                                                 // убираем все повторения (чтобы не было одинаковых записей "преподаватель - дисциплина")
                    .ToList();

                var teachersInApiTable = teachersAndSubjects
                    .Select(entry => entry.Teacher)
                    .ToList();

                // Выбираем дисциплины из api table
                var subjectsInApiTable = teachersAndSubjects
                    .Select(entry => entry.Discipline)
                    .ToList();

                // Выбираем названия дисциплин, которые соответствуют дисциплинам из api table из БД (если дисциплина == null, то ее не выбираем, так как ее нет в учебном плане)
                var subjectsInCurriculum = await _dataContext.SubjectMappings
                    .Where(x => subjectsInApiTable.Contains(x.SubjectNameApiTable) && x.SubjectNameCurriculum != null)
                    .Select(x => x.SubjectNameCurriculum)
                    .ToListAsync();

                // ТУТ СОЗДАНИЕ УЧИТЕЛЕЙ, ПРЕДМЕТОВ, СВЯЗЕЙ МЕЖДУ НИМИ И ТД, ПОТОМ УЖЕ ИЩЕМ СЕМЕСТР И ЧАСЫ И ПРИСВАИВАЕМ ЕГО ВСЕМ ПРЕДМЕТАМ
                // -------------

                // -------------

                // P.S. В БУДУЩЕМ НА ВСЯКИЙ ПОМЕНЯТЬ У ГРУППЫ ДОКУМЕНТ НА NOT NULL, ЩАС СДЕЛАТЬ НЕЛЬЗЯ ПОТОМУ ЧТО В ДАТАСИДЕ ЕСТЬ ГРУППЫ С NULL ДОКУМЕНТОМ
                var curriculumAnalyzer = new CurriculumAnalyzer(group.CurriculumExcelDocument);
                int commonSemester = curriculumAnalyzer.GetCommonSemesterForSubjects(subjectsInCurriculum!);

                foreach (var subject in subjectsInCurriculum)
                {
                    int numberOfLaboratoryLesson = curriculumAnalyzer.GetNumberOfLaboratoryLesson(subject!, commonSemester);
                }
            }

            return Ok();
        }
    }
}
