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

        [HttpPost("{groupName}")]
        public async Task<ActionResult> UploadGroupWithTeachersAndSubjects(string groupName)
        {
            var response = await _httpClient.GetAsync("https://apitable.astu.org/search/get?q=ДИПРБ_11/1&t=group");
            var groupLessonsDTO = response.Content.ReadFromJsonAsync<GroupWithLessonsDTO>();

            return Ok();
        }
    }
}
