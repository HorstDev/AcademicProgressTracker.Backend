﻿using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Application.Common.Interfaces.Repositories;
using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Application.Common.Schedule;
using AcademicProgressTracker.Application.Common.ViewModels.Group;
using AcademicProgressTracker.Application.Common.ViewModels.Report;
using AcademicProgressTracker.Application.Curriculum;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;
        private readonly HttpClient _httpClient;
        private readonly IAuthService _authService;     // нужен для регистрации новых преподавателей и студентов (в будущем убрать в другой сервис)
        private readonly IScheduleAnalyzer _scheduleAnalyzer;
        private readonly IGroupRepository _groupRepository;

        public GroupController(AcademicProgressDataContext dataContext, HttpClient httpClient, 
            IAuthService authService, IScheduleAnalyzer scheduleAnalyzer, IGroupRepository groupRepository)
        {
            _dataContext = dataContext;
            _httpClient = httpClient;
            _authService = authService;
            _scheduleAnalyzer = scheduleAnalyzer;
            _groupRepository = groupRepository;
        }

        /// <summary>
        /// Получение списка всех групп
        /// </summary>
        /// <returns>Все группы</returns>
        [HttpGet("all-groups")]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> GetAllGroups()
        {
            var allGroups = await _dataContext.Groups.OrderBy(group => group.Name).ToListAsync();

            var allGroupsVm = allGroups.Select(group => new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                DateTimeOfUpdateDependenciesFromServer = group.DateTimeOfUpdateDependenciesFromServer,
            }).ToList();

            return allGroupsVm;
        }

        [HttpGet("groups-by-substring/{substring}")]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> GetFiveGroups(string substring)
        {
            var groups = await _dataContext.Groups
                .Where(group => group.Name.Contains(substring))
                .Take(5)
                .ToListAsync();

            var groupsVm = groups.Select(group => new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                DateTimeOfUpdateDependenciesFromServer = group.DateTimeOfUpdateDependenciesFromServer,
            }).ToList();

            return groupsVm;
        }

        [HttpGet("groups-by-substring-from-api-table/{substring}")]
        public async Task<ActionResult<IEnumerable<GroupViewModel>>> GetFiveGroupsFromApiTable(string substring)
        {
            var groupsVm = new List<GroupViewModel>();
            var response = await _httpClient.GetAsync($"https://apitable.astu.org/search/find?q=${substring}");
            if (response.IsSuccessStatusCode)
                groupsVm = await response.Content.ReadFromJsonAsync<List<GroupViewModel>>();
            else
                return StatusCode((int)response.StatusCode, "Сервер АГТУ недоступен");

            if (groupsVm == null)
                return StatusCode((int)response.StatusCode, "Bad Gateway");
            return groupsVm;
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupViewModel>> GetGroup(Guid groupId)
        {
            var group = await _dataContext.Groups
                .SingleAsync(group => group.Id == groupId);

            var groupVm = new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name,
                DateTimeOfUpdateDependenciesFromServer = group.DateTimeOfUpdateDependenciesFromServer,
            };

            return groupVm;
        }

        [HttpDelete("{groupId}")]
        public async Task<ActionResult> DeleteGroup(Guid groupId)
        {
            var group = await _dataContext.Groups
                .SingleAsync(group => group.Id == groupId);

            await _groupRepository.DeleteAsync(group);

            return Ok();
        }

        /// <summary>
        /// Загрузка новой группы
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="excelCurriculum">Учебный план</param>
        /// <returns></returns>
        [HttpPost("{groupName}")]
        public async Task<ActionResult> Create(string groupName, IFormFile excelCurriculum)
        {
            // P.S. НАДО БУДЕТ ДОБАВИТЬ УНИКАЛЬНОСТЬ В КОНФИГУРАЦИИ БД ДЛЯ НАЗВАНИЯ ГРУППЫ ИНАЧЕ ПЛОХО
            if (excelCurriculum != null && excelCurriculum.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    excelCurriculum.CopyTo(memoryStream);
                    var curriculumExcelDocumentBytes = memoryStream.ToArray();
                    var group = new Group(Uri.UnescapeDataString(groupName), curriculumExcelDocumentBytes);

                    await _dataContext.AddAsync(group);
                    await _dataContext.SaveChangesAsync();
                    // NEW GROUP URI. MAKE IT LATER
                    return CreatedAtAction("Create", new { id = group.Id }, group);
                }
            }

            return BadRequest("Ошибка. Файл пуст.");
        }

        /// <summary>
        /// Загрузка зависимостей для группы (учителя и дисциплины)
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="semesterStart"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpPost("{groupId}/upload-dependencies/{semesterStart}")]
        public async Task<ActionResult> UploadTeachersAndSubjects(Guid groupId, DateOnly semesterStart)
        {
            GroupWithLessonsDTO? groupLessonsDTO;
            var group = await _dataContext.Groups.SingleAsync(x => x.Id == groupId);
            // Обновляем дату последнего обновления информации с сервера
            group.DateTimeOfUpdateDependenciesFromServer = DateTime.Now;
            var response = await _httpClient.GetAsync($"https://apitable.astu.org/search/get?q={group.Name}&t=group");
            if(response.IsSuccessStatusCode)
                groupLessonsDTO = await response.Content.ReadFromJsonAsync<GroupWithLessonsDTO>();
            else
                return StatusCode((int)response.StatusCode, "Сервер АГТУ недоступен");

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
                    .Distinct()
                    .ToList();

                // Выбираем дисциплины из api table
                var subjectsInApiTable = teachersAndSubjects
                    .Select(entry => entry.Discipline)
                    .Distinct()
                    .ToList();

                // Выбираем названия дисциплин, которые соответствуют дисциплинам из api table из БД
                var subjectsMappings = await _dataContext.SubjectMappings
                    .Where(x => subjectsInApiTable.Contains(x.SubjectNameApiTable))
                    .ToListAsync();

                if (subjectsMappings.Count < subjectsInApiTable.Count)
                    throw new BadHttpRequestException($"Ошибка: в таблице 'ПредметСервер - ПредметУчебныйПлан' не хватает предметов для группы {group.Name}");

                var subjectsInCurriculum = subjectsMappings
                    .Select(x => x.SubjectNameCurriculum)
                    .ToList();

                var curriculumAnalyzer = new CurriculumAnalyzer(group.CurriculumExcelDocument);

                // Извлекаем для каждой дисциплины из api те дисциплины, которые есть в учебном плане и с помощью них находим общий семестр
                var subjectsExistsInCurriculum = subjectsMappings
                    .Where(x => x.SubjectNameCurriculum != null)
                    .Select(x => x.SubjectNameCurriculum)
                    .ToList();
                int commonSemester = curriculumAnalyzer.GetCommonSemesterForSubjects(subjectsInCurriculum!);

                // Проверяем, есть ли с этим общим семестром дисциплины у группы, если есть, то они уже загружены
                var testSubject = await _dataContext.Subjects.FirstOrDefaultAsync(x => x.GroupId == group.Id && x.Semester == commonSemester);
                if (testSubject != null)
                    throw new BadHttpRequestException($"У группы {group.Name} уже загружены дисциплины за {commonSemester} семестр!");

                // 1. Добавляем предметы и их лабораторные занятия и статусы к ним для каждого студента (если предмета нет в учебном плане, то и лабораторных занятий нет)
                var subjectsToDatabase = new List<Subject>();
                var studentsOfGroup = await _dataContext.UserGroup.Where(x => x.GroupId == groupId && x.Role!.Name == "Student")
                    .Select(x => x.User)
                    .ToListAsync();

                foreach (var subjectApiTable in subjectsInApiTable)
                {
                    // Добавляем занятия для предмета
                    var lessons = new List<Lesson>();
                    var subjectCurriculum = subjectsMappings.Single(x => x.SubjectNameApiTable == subjectApiTable).SubjectNameCurriculum;

                    if (subjectCurriculum != null)
                    {
                        int labLessonCount = curriculumAnalyzer.GetNumberOfLaboratoryLesson(subjectCurriculum, commonSemester);
                        int lectureLessonCount = curriculumAnalyzer.GetNumberOfLectureLesson(subjectCurriculum, commonSemester);
                        int practiceLessonCount = curriculumAnalyzer.GetNumberOfPracticeLesson(subjectCurriculum, commonSemester);

                        lessons = _scheduleAnalyzer.GetLessonsOfSubjectFromLessonsDTO(semesterStart, groupLessonsDTO.Lessons, subjectApiTable,
                            labLessonCount, lectureLessonCount, practiceLessonCount);
                    }

                    // Добавляем предметы
                    subjectsToDatabase.Add(new Subject
                    {
                        Name = subjectApiTable,
                        Group = group,
                        Semester = commonSemester,
                        Lessons = lessons
                    });
                }
                // Добавляем предметы в БД
                await _dataContext.Subjects.AddRangeAsync(subjectsToDatabase);

                // 2. Добавляем преподавателей
                var teacherUsersToDatabase = new List<User>();
                // Выбираем преподавателей из группы, которые уже загружены в БД
                var teachersDatabase = await _dataContext.TeacherProfiles
                    .Where(x => teachersInApiTable.Contains(x.Name))
                    .Include(x => x.User)
                    .ToListAsync();
                var teachersDatabaseNames = teachersDatabase.Select(x => x.Name);

                foreach(var teacherName in teachersInApiTable)
                {
                    // Выбираем дисциплины для преподавателя из subjects
                    var subjectsNamesForThisTeacher = teachersAndSubjects.Where(x => x.Teacher == teacherName).Select(x => x.Discipline);
                    var subjectsForThisTeacherInDatabase = subjectsToDatabase.Where(x => subjectsNamesForThisTeacher.Contains(x.Name));
                    // Если преподаватель из api table есть в базе данных, то просто добавляем ему новые дисциплины
                    if (teachersDatabaseNames.Contains(teacherName))
                    {
                        foreach(var subject in subjectsForThisTeacherInDatabase)
                        {
                            teachersDatabase.Single(x => x.Name == teacherName).User!.Subjects.Add(subject);
                        }
                    }
                    else    // иначе создаем новый аккаунт для преподавателя и профиль преподавателя
                    {
                        var teacherUser = await _authService.GetTeacherUserWithRandomLoginAndPasswordAsync(teacherName);
                        foreach (var subject in subjectsForThisTeacherInDatabase)
                        {
                            teacherUser.Subjects.Add(subject);
                        }
                        // Добавляем новых преподавателей в БД
                        teacherUsersToDatabase.Add(teacherUser);
                    }
                }
                // Добавляем преподавателей с зависимостями (профили учителей, предметы) в БД
                await _dataContext.Users.AddRangeAsync(teacherUsersToDatabase);
                // Сохраняем все изменения, которые были сделаны
                await _dataContext.SaveChangesAsync();
                return Created();
            }

            return StatusCode(502, "Bad Gateway");
        }

        /// <summary>
        /// Загрузка зависимостей для группы из json файла
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="semesterStart"></param>
        /// <param name="jsonFile"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpPost("{groupId}/upload-dependencies-from-file/{semesterStart}")]
        public async Task<ActionResult> UploadTeachersAndSubjectsFromFile(Guid groupId, DateOnly semesterStart, IFormFile jsonFile)
        {
            var group = await _dataContext.Groups.SingleAsync(x => x.Id == groupId);
            group.DateTimeOfUpdateDependenciesFromServer = DateTime.Now;
            var groupLessonsDTO = new GroupWithLessonsDTO();

            string jsonString;
            using(var streamReader = new StreamReader(jsonFile.OpenReadStream()))
            {
                jsonString = await streamReader.ReadToEndAsync();
            }

            // Определить параметры сериализации
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true, // Учесть регистр имен свойств
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) } // Опционально, если используются enum
            };

            // Десериализовать JSON
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                var root = document.RootElement;
                groupLessonsDTO = JsonSerializer.Deserialize<GroupWithLessonsDTO>(root.GetRawText(), options);
            }

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
                    .Distinct()
                    .ToList();

                // Выбираем дисциплины из api table
                var subjectsInApiTable = teachersAndSubjects
                    .Select(entry => entry.Discipline)
                    .Distinct()
                    .ToList();

                // Выбираем названия дисциплин, которые соответствуют дисциплинам из api table из БД
                var subjectsMappings = await _dataContext.SubjectMappings
                    .Where(x => subjectsInApiTable.Contains(x.SubjectNameApiTable))
                    .ToListAsync();

                if (subjectsMappings.Count < subjectsInApiTable.Count)
                    throw new BadHttpRequestException($"Ошибка: в таблице 'ПредметСервер - ПредметУчебныйПлан' не хватает предметов для группы {group.Name}");

                var subjectsInCurriculum = subjectsMappings
                    .Select(x => x.SubjectNameCurriculum)
                    .ToList();

                var curriculumAnalyzer = new CurriculumAnalyzer(group.CurriculumExcelDocument);

                // Извлекаем для каждой дисциплины из api те дисциплины, которые есть в учебном плане и с помощью них находим общий семестр
                var subjectsExistsInCurriculum = subjectsMappings
                    .Where(x => x.SubjectNameCurriculum != null)
                    .Select(x => x.SubjectNameCurriculum)
                    .ToList();
                int commonSemester = curriculumAnalyzer.GetCommonSemesterForSubjects(subjectsInCurriculum!);

                // Проверяем, есть ли с этим общим семестром дисциплины у группы, если есть, то они уже загружены
                var testSubject = await _dataContext.Subjects.FirstOrDefaultAsync(x => x.GroupId == group.Id && x.Semester == commonSemester);
                if (testSubject != null)
                    throw new BadHttpRequestException($"У группы {group.Name} уже загружены дисциплины за {commonSemester} семестр!");

                // 1. Добавляем предметы и их лабораторные занятия и статусы к ним для каждого студента (если предмета нет в учебном плане, то и лабораторных занятий нет)
                var subjectsToDatabase = new List<Subject>();
                var studentsOfGroup = await _dataContext.UserGroup.Where(x => x.GroupId == groupId && x.Role!.Name == "Student")
                    .Select(x => x.User)
                    .ToListAsync();

                foreach (var subjectApiTable in subjectsInApiTable)
                {
                    // Добавляем занятия для предмета
                    var lessons = new List<Lesson>();
                    var subjectCurriculum = subjectsMappings.Single(x => x.SubjectNameApiTable == subjectApiTable).SubjectNameCurriculum;

                    if (subjectCurriculum != null)
                    {
                        int labLessonCount = curriculumAnalyzer.GetNumberOfLaboratoryLesson(subjectCurriculum, commonSemester);
                        int lectureLessonCount = curriculumAnalyzer.GetNumberOfLectureLesson(subjectCurriculum, commonSemester);
                        int practiceLessonCount = curriculumAnalyzer.GetNumberOfPracticeLesson(subjectCurriculum, commonSemester);

                        lessons = _scheduleAnalyzer.GetLessonsOfSubjectFromLessonsDTO(semesterStart, groupLessonsDTO.Lessons, subjectApiTable,
                            labLessonCount, lectureLessonCount, practiceLessonCount);
                    }

                    // Добавляем предметы
                    subjectsToDatabase.Add(new Subject
                    {
                        Name = subjectApiTable,
                        Group = group,
                        Semester = commonSemester,
                        Lessons = lessons
                    });
                }
                // Добавляем предметы в БД
                await _dataContext.Subjects.AddRangeAsync(subjectsToDatabase);

                // 2. Добавляем преподавателей
                var teacherUsersToDatabase = new List<User>();
                // Выбираем преподавателей из группы, которые уже загружены в БД
                var teachersDatabase = await _dataContext.TeacherProfiles
                    .Where(x => teachersInApiTable.Contains(x.Name))
                    .Include(x => x.User)
                    .ToListAsync();
                var teachersDatabaseNames = teachersDatabase.Select(x => x.Name);

                foreach (var teacherName in teachersInApiTable)
                {
                    // Выбираем дисциплины для преподавателя из subjects
                    var subjectsNamesForThisTeacher = teachersAndSubjects.Where(x => x.Teacher == teacherName).Select(x => x.Discipline);
                    var subjectsForThisTeacherInDatabase = subjectsToDatabase.Where(x => subjectsNamesForThisTeacher.Contains(x.Name));
                    // Если преподаватель из api table есть в базе данных, то просто добавляем ему новые дисциплины
                    if (teachersDatabaseNames.Contains(teacherName))
                    {
                        foreach (var subject in subjectsForThisTeacherInDatabase)
                        {
                            teachersDatabase.Single(x => x.Name == teacherName).User!.Subjects.Add(subject);
                        }
                    }
                    else    // иначе создаем новый аккаунт для преподавателя и профиль преподавателя
                    {
                        var teacherUser = await _authService.GetTeacherUserWithRandomLoginAndPasswordAsync(teacherName);
                        foreach (var subject in subjectsForThisTeacherInDatabase)
                        {
                            teacherUser.Subjects.Add(subject);
                        }
                        // Добавляем новых преподавателей в БД
                        teacherUsersToDatabase.Add(teacherUser);
                    }
                }
                // Добавляем преподавателей с зависимостями (профили учителей, предметы) в БД
                await _dataContext.Users.AddRangeAsync(teacherUsersToDatabase);
                // Сохраняем все изменения, которые были сделаны
                await _dataContext.SaveChangesAsync();
                return Created();
            }

            return StatusCode(502, "Bad Gateway");
        }
        
        /// <summary>
        /// Получение курируемой группы
        /// </summary>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        [HttpGet("supervised-groups"), Authorize(Roles = "Teacher")]
        public async Task<ActionResult<List<GroupViewModel>>> GetSupervisedGroup()
        {
            var teacherId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            // Получаем группы, которые курируются преподавателем
            var groups = await _dataContext.UserGroup
                .Where(x => x.UserId == teacherId && x.Role!.Name == "Teacher")
                .Select(x => x.Group)
                .ToListAsync();

            if (groups == null || !groups.Any())
                throw new BadHttpRequestException("Курируемые группы отсутствуют!");

            List<GroupViewModel> supervisedGroups = new List<GroupViewModel>();
            foreach(var group in groups)
            {
                if (group == null) continue;

                supervisedGroups.Add(new GroupViewModel
                {
                    Id = group.Id,
                    Name = group.Name,
                    DateTimeOfUpdateDependenciesFromServer = group.DateTimeOfUpdateDependenciesFromServer
                });
            }

            return supervisedGroups;
        }
    }
}
