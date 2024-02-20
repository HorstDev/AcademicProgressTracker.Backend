using AcademicProgressTracker.Application.Common.DTOs;
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

        // Загрузка учебного плана для созданной ранее группы
        [HttpPost("{groupId}/uploadСurriculum")]
        public async Task<ActionResult> UploadСurriculum(Guid groupId, IFormFile excelCurriculum)
        {
            if (excelCurriculum != null && excelCurriculum.Length > 0)
            {
                var group = _dataContext.Groups.Single(x => x.Id == groupId);

                using (var memoryStream = new MemoryStream())
                {
                    excelCurriculum.CopyTo(memoryStream);
                    var curriculumExcelDocumentBytes = memoryStream.ToArray();
                    group.CurriculumExcelDocument = curriculumExcelDocumentBytes;
                    _dataContext.Update(group);
                    await _dataContext.SaveChangesAsync();
                    return Ok($"Учебный план {excelCurriculum.FileName} успешно загружен для группы {group.Name}.");
                }
            }

            return BadRequest("Ошибка. Файл пуст.");
        }

        // Создание группы (название берется с сервера АГТУ)
        [HttpPost("{groupName}")]
        public async Task<ActionResult> UploadGroupWithTeachersAndSubjects(string groupName)
        {
            var response = await _httpClient.GetAsync("https://apitable.astu.org/search/get?q=ДИПРБ_41/1&t=group");
            var groupLessonsDTO = await response.Content.ReadFromJsonAsync<GroupWithLessonsDTO>();

            if (groupLessonsDTO != null)
            {
                var teachersAndSubjects = groupLessonsDTO.Lessons
                    .SelectMany(lesson => lesson.Entries)                       // выбираем все записи (entries) из всех занятий
                    .Select(entry => new { entry.Teacher, entry.Discipline })   // выбираем из всех записей учителя и соответствующую ему дисциплину
                    .Distinct()                                                 // убираем все повторения (чтобы не было одинаковых записей "преподаватель - дисциплина")
                    .ToList();

                //var uniqueTeacherDisciplineCombinations = groupLessonsDTO.Lessons
                //    .SelectMany(lesson => lesson.Entries);
                    //.Select(entry => new { Teacher = entry.Teacher, Discipline = entry.Discipline })
                    //.Distinct();
            }

            return Ok();
        }
    }
}
