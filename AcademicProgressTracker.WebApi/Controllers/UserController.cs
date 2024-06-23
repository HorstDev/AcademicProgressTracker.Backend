using AcademicProgressTracker.Application.Common.Interfaces.Services;
using AcademicProgressTracker.Application.Common.ViewModels.Subject;
using AcademicProgressTracker.Application.Common.ViewModels.User;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AcademicProgressDataContext _dataContext;
        private readonly IAuthService _authService;

        public UserController(AcademicProgressDataContext dataContext, IAuthService authServise)
        {
            _dataContext = dataContext;
            _authService = authServise;
        }

        /// <summary>
        /// Получение списка пользователей, имя или логин которых содержат подстроку
        /// </summary>
        /// <param name="substringName">Подстрока</param>
        /// <returns>Список пользователей</returns>
        [HttpGet("{substringName}")]
        public async Task<IEnumerable<UserViewModel>> GetBySubstring(string substringName)
        {
            var users = await _dataContext.Users
                .Where(user => user.Email.ToLower().Contains(substringName.ToLower()) 
                    || user.Profiles.Any(profile => profile.Name.ToLower().Contains(substringName.ToLower()))) 
                .Include(user => user.Profiles)
                .Include(user => user.Roles)
                .ToListAsync();

            var usersVm = users.Select(user => new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Profiles = user.Profiles.Select(profile => new UserProfileViewModel
                {
                    Id = profile.Id,
                    Name = profile.Name,
                }).ToList(),
                Roles = user.Roles.Select(role => new UserRoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                }).ToList(),
            });

            return usersVm;
        }

        [HttpPut("make-user-admin/{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserViewModel>> MakeAdmin(Guid userId)
        {
            var user = await _dataContext.Users
                .Include(user => user.Profiles)
                .Include(user => user.Roles)
                .SingleAsync(user => user.Id == userId);

            var adminRole = await _dataContext.Roles.SingleAsync(role => role.Name == "Admin");
            user.Roles.Add(adminRole);
            await _dataContext.SaveChangesAsync();

            var userVm = new UserViewModel
            {
                Id = userId,
                Email = user.Email,
                Profiles = user.Profiles.Select(profile => new UserProfileViewModel
                {
                    Id = profile.Id,
                    Name = profile.Name,
                }).ToList(),
                Roles = user.Roles.Select(role => new UserRoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                }).ToList(),
            };

            return userVm;
        }

        [HttpPut("make-user-no-admin/{userId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserViewModel>> MakeNoAdmin(Guid userId)
        {
            var user = await _dataContext.Users
                .Include(user => user.Profiles)
                .Include(user => user.Roles)
                .SingleAsync(user => user.Id == userId);

            var adminRole = await _dataContext.Roles.SingleAsync(role => role.Name == "Admin");
            user.Roles.Remove(adminRole);
            await _dataContext.SaveChangesAsync();

            var userVm = new UserViewModel
            {
                Id = userId,
                Email = user.Email,
                Profiles = user.Profiles.Select(profile => new UserProfileViewModel
                {
                    Id = profile.Id,
                    Name = profile.Name,
                }).ToList(),
                Roles = user.Roles.Select(role => new UserRoleViewModel
                {
                    Id = role.Id,
                    Name = role.Name,
                }).ToList(),
            };

            return userVm;
        }

        [HttpGet("teachers-by-substring/{substring}")]
        public async Task<IEnumerable<UserProfileViewModel>> GetFiveTeachersBySubstring(string substring)
        {
            var teachers = await _dataContext.TeacherProfiles
                .Where(profile => profile.Name.ToLower().Contains(substring.ToLower()))
                .Take(5)
                .ToListAsync();

            var userProfiles = teachers.Select(teacher => new UserProfileViewModel
            {
                Id = teacher.UserId,
                Name = teacher.Name,
            });

            return userProfiles;
        }

        [HttpGet("students/{groupId}")]
        public async Task<IEnumerable<UserProfileViewModel>> GetStudentsByGroup(Guid groupId)
        {
            // Выбираем всех студентов у группы
            var studentProfiles = await _dataContext.UserGroup
                .Where(x => x.GroupId == groupId && x.Role!.Name == "Student")
                .Select(x => x.User!.Profiles.OfType<StudentProfile>().FirstOrDefault())
                .ToListAsync();

            var studentProfilesVm = new List<UserProfileViewModel>();
            foreach(var studentProfile in studentProfiles)
            {
                if (studentProfile != null)
                {
                    studentProfilesVm.Add(new UserProfileViewModel
                    {
                        Id = studentProfile.UserId,
                        Name = studentProfile.Name,
                    });
                }
            }

            return studentProfilesVm.OrderBy(profile => profile.Name);
        }

        /// <summary>
        /// Список преподавателей в группе
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("teachers/{groupId}")]
        public async Task<IEnumerable<UserProfileViewModel>> GetTeachersByGroup(Guid groupId)
        {
            // Получаем преподавателей в группе
            var teacherProfiles = await _dataContext.Groups
                .SelectMany(group => group.Subjects
                    .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester) && subject.GroupId == groupId))
                .SelectMany(subject => subject.Users)
                    .Select(user => user.Profiles.OfType<TeacherProfile>().FirstOrDefault())
                .Distinct()
                .ToListAsync();

            return teacherProfiles.Select(profile => new UserProfileViewModel
            {
                Id = profile!.UserId,
                Name = profile.Name
            });
        }

        [HttpGet("curator/{groupId}")]
        public async Task<ActionResult<UserProfileViewModel>> GetCuratorByGroup(Guid groupId)
        {
            // Выбираем куратора группы
            var curatorProfile = await _dataContext.UserGroup
                .Where(x => x.GroupId == groupId && x.Role!.Name == "Teacher")
                .Select(x => x.User!.Profiles.OfType<TeacherProfile>().FirstOrDefault())
                .SingleOrDefaultAsync();

            var curatorProfileVm = new UserProfileViewModel();
            if (curatorProfile != null)
            {
                curatorProfileVm.Id = curatorProfile.UserId;
                curatorProfileVm.Name = curatorProfile.Name;
                return Ok(curatorProfileVm);
            }

            return NotFound("Куратор не найден!");
        }

        [HttpPost("add-curator/{userId}/{groupId}")]
        public async Task<ActionResult> AddCuratorToGroup(Guid userId, Guid groupId)
        {
            // Находим преподавателя вместе с ролью, чтобы проверить роль на следующих шагах
            var teacher = await _dataContext.Users
                .Include(user => user.Roles)
                .SingleAsync(user => user.Id == userId);
            
            // Если у пользователя нет роли преподавателя
            if (!teacher.Roles.Any(role => role.Name == "Teacher"))
                throw new BadHttpRequestException("Пользователь не является преподавателем! Сделать куратором невозможно");

            // Выбираем существующего куратора группы.
            var currentCurator = await _dataContext.UserGroup
                .Where(x => x.GroupId == groupId && x.Role!.Name == "Teacher")
                .SingleOrDefaultAsync();

            // На случай, если он имеется, удаляем его (т.к. куратор может быть только один)
            if (currentCurator != null)
                _dataContext.UserGroup.Remove(currentCurator);

            // Добавляем нового куратора группы
            await _dataContext.UserGroup.AddAsync(new Persistence.Models.UserGroup
            {
                User = teacher,
                GroupId = groupId,
                Role = teacher.Roles.Single(role => role.Name == "Teacher"),
            });

            await _dataContext.SaveChangesAsync();

            return Created();
        }

        /// <summary>
        /// Добавление студента в группу
        /// При добавлении студента в группу добавляются и статусы к лабораторным работам, которые уже существуют в этом семестре
        /// </summary>
        /// <param name="studentName"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpPost("add-student/{studentName}/{groupId}")]
        public async Task<ActionResult<UserProfileViewModel>> AddStudent(string studentName, Guid groupId)
        {
            User studentUser = await _authService.GetStudentUserWithRandomLoginAndPasswordAsync(studentName);

            // Извлекаем все лабораторные работы для всех предметов в этой группе в текущем семестре
            var labWorks = await _dataContext.Groups
                .Where(group => group.Id == groupId)
                .SelectMany(group => group.Subjects
                    .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester)))
                .SelectMany(subject => subject.Lessons.OfType<LabLesson>())
                .Select(labLesson => labLesson.LabWork)
                .Where(labWork => labWork != null)
                .Distinct()
                .ToListAsync();

            // Извлекаем все занятия для всех предметов в этой группе в текущем семестре
            var startedLessons = await _dataContext.Groups
                .Where(group => group.Id == groupId)
                .SelectMany(group => group.Subjects
                    .Where(subject => subject.Semester == group.Subjects.Max(x => x.Semester)))
                .SelectMany(subject => subject.Lessons)
                .Where(lesson => lesson.IsStarted)
                .ToListAsync();

            // Добавляем аккаунт студента
            Guid studentId = new Guid();
            studentUser.Id = studentId;
            await _dataContext.Users.AddAsync(studentUser);

            // Добавляем студента к группе
            await _dataContext.UserGroup.AddAsync(new Persistence.Models.UserGroup
            {
                GroupId = groupId,
                User = studentUser,
                Role = studentUser.Roles.First(),
            });

            // Для каждой ЛР добавляем статус для нового студента
            foreach (var labWork in labWorks)
            {
                await _dataContext.LabWorkUserStatuses.AddAsync(new LabWorkUserStatus
                {
                    User = studentUser,
                    LabWork = labWork,
                    IsDone = false,
                });
            }

            // Для каждого проведенного занятия добавляем статус, что добавляемый студент эти занятия не посещал
            foreach (var startedLesson in startedLessons)
            {
                await _dataContext.LessonUserStatuses.AddAsync(new LessonUserStatus
                {
                    User = studentUser,
                    Lesson = startedLesson,
                    IsVisited = false,
                });
            }

            await _dataContext.SaveChangesAsync();

            return new UserProfileViewModel
            {
                Id = studentId,
                Name = studentUser.Profiles.First().Name,
            };
        }
    }
}
