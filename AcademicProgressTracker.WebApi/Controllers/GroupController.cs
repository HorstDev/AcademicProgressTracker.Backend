using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            var group = new Group { Name = groupName };

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
        [HttpPost("{groupName}/UploadDependencies")]
        public async Task<ActionResult> UploadTeachersAndSubjects(string groupName)
        {
            var group = _dataContext.Groups.Single(x => x.Name == groupName);
            var response = await _httpClient.GetAsync("https://apitable.astu.org/search/get?q=ДИПРБ_41/1&t=group");
            var groupLessonsDTO = await response.Content.ReadFromJsonAsync<GroupWithLessonsDTO>();

            if (groupLessonsDTO != null)
            {
                // Выбираем преподавателей и соответствующие им дисциплины
                var teachersAndSubjects = groupLessonsDTO.Lessons
                    .SelectMany(lesson => lesson.Entries)                       // выбираем все записи (entries) из всех занятий
                    .Select(entry => new EntryDTO { Teacher = entry.Teacher, Discipline = entry.Discipline })   // выбираем из всех записей учителя и соответствующую ему дисциплину
                    .Distinct()                                                 // убираем все повторения (чтобы не было одинаковых записей "преподаватель - дисциплина")
                    .ToList();

                // Выбираем дисциплины из api table
                var subjectsInApiTable = teachersAndSubjects
                    .Select(entry => entry.Discipline)
                    .ToList();

                // Выбираем названия дисциплин, которые соответствуют дисциплинам из api table из БД (если дисциплина == null, то ее не выбираем, так как ее нет в учебном плане)
                var subjectInCurriculum = _dataContext.SubjectMappings
                    .Where(x => subjectsInApiTable.Contains(x.SubjectNameApiTable) && x.SubjectNameCurriculum != null)
                    .Select(x => x.SubjectNameCurriculum)
                    .ToList();
            }

            return Ok();
        }
    }
}
