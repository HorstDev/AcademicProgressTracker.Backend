using AcademicProgressTracker.Application.Common.ViewModels.Subject;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using AcademicProgressTracker.Persistence.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;

        public SubjectController(AcademicProgressDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        /// <summary>
        /// Получение списка преподаваемых дисциплин
        /// </summary>
        /// <returns>Преподаваемые дисциплины</returns>
        [HttpGet("taught-subjects"), Authorize(Roles = "Teacher")]
        public async Task<IEnumerable<SubjectViewModel>> GetTaughtSubjects()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Сначала находим все группы, принадлежащие каждому преподавателю
            // Затем у каждой такой группы находим предметы с максимальным семестром (текущим) и принадлежащие преподавателю и формируем viewModel
            return await _dataContext.Groups
                .Where(group => group.Subjects.Any(subject => subject.Users.Any(user => user.Id == teacherId)))
                .SelectMany(group => group.Subjects
                    .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester) && subject.Users.Any(user => user.Id == teacherId))
                    .Select(subject => new SubjectViewModel
                    {
                        Id = subject.Id,
                        Name = subject.Name,
                        GroupName = group.Name
                    }))
                .OrderBy(subjectViewModel => subjectViewModel.GroupName)
                .ToListAsync();
        }

        [HttpGet("subjects/{groupId}")]
        public async Task<IEnumerable<SubjectViewModel>> GetSubjectsByGroupId(Guid groupId)
        {
            var subjects = await _dataContext.Groups
            .SelectMany(group => group.Subjects
                .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester) && subject.GroupId == groupId))
                .ToListAsync();

            return subjects.Select(subject => new SubjectViewModel
            {
                Id = subject.Id,
                Name = subject.Name,
            });
        }

        [HttpGet("subject-mappings"), Authorize(Roles = "Admin")]
        public async Task<IEnumerable<SubjectMapping>> GetSubjectMappings()
        {
            return await _dataContext.SubjectMappings.ToListAsync();
        }

        [HttpPost("subject-mapping"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> AddSubjectMapping(SubjectMapping subjectMapping)
        {
            if (string.IsNullOrEmpty(subjectMapping.SubjectNameApiTable))
                throw new BadHttpRequestException("Поле с названием предмета на сервере пустое!");
            // Делаем поле для названия предмета в учебном плане null, если оно пустое
            if (string.IsNullOrEmpty(subjectMapping.SubjectNameCurriculum))
                subjectMapping.SubjectNameCurriculum = null;

            await _dataContext.SubjectMappings.AddAsync(subjectMapping);
            await _dataContext.SaveChangesAsync();

            return Created();
        }

        [HttpPut("subject-mapping"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<SubjectMapping>> UpdateSubjectMapping(SubjectMapping subjectMapping)
        {
            if (string.IsNullOrEmpty(subjectMapping.SubjectNameApiTable))
                throw new BadHttpRequestException("Поле с названием предмета на сервере пустое!");

            // Делаем поле для названия предмета в учебном плане null, если оно пустое
            if (string.IsNullOrEmpty(subjectMapping.SubjectNameCurriculum))
                subjectMapping.SubjectNameCurriculum = null;

            _dataContext.SubjectMappings.Update(subjectMapping);
            await _dataContext.SaveChangesAsync();

            return Ok(subjectMapping);
        }

        [HttpDelete("subject-mapping/{subjectMappingId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteSubjectMapping(Guid subjectMappingId)
        {
            var subjectMapping = await _dataContext.SubjectMappings.SingleAsync(x => x.Id == subjectMappingId);

            _dataContext.SubjectMappings.Remove(subjectMapping);
            await _dataContext.SaveChangesAsync();

            return Ok();
        }
    }
}
