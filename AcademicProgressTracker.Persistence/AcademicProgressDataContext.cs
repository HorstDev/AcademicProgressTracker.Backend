using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence.EntityTypeConfigurations;
using AcademicProgressTracker.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.Persistence
{
    public class AcademicProgressDataContext : DbContext
    {
        // Аккаунты, роли, профили
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<TeacherProfile> TeacherProfiles { get; set; }
        public DbSet<StudentProfile> StudentProfiles { get; set; }
        // Связи много ко многим
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TeacherSubject> TeacherSubject { get; set; }
        // Прочее
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        // Лабораторные
        public DbSet<LabWork> LabWorks { get; set; }
        public DbSet<LabWorkUserStatus> LabWorkUserStatuses { get; set; }
        // Занятия
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LabLesson> LabLessons { get; set; }
        public DbSet<LectureLesson> LectureLessons { get; set; }
        public DbSet<PracticeLesson> PracticeLessons { get; set; }
        public DbSet<LessonUserStatus> LessonUserStatuses { get; set; }
        // Маппинг предметов
        public DbSet<SubjectMapping> SubjectMappings { get; set; }

        public AcademicProgressDataContext(DbContextOptions<AcademicProgressDataContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Сначала получаем данные, потом применяем конфигурации, потом устанавливаем данные в БД с примененными конфигурациями
            // Получаем данные
            var roles = GetRoles();
            var users = GetUsers();
            //var groups = GetGroups();
            //var subjects = GetSubjects(groups);
            //var labworks = GetLabWorks(subjects);
            //var labworkstatuses = GetLabWorkStatuses(labworks, users);
            var subjectMappings = GetSubjectMappings();

            // Применяем конфигурации
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupConfiguration(roles.Single(x => x.Name == "Student").Id));
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            //modelBuilder.Entity<LabWorkStatus>()
            //    .Property(x => x.CurrentScore)
            //    .HasColumnType("numeric(18, 2)");
            //modelBuilder.Entity<LabWork>()
            //    .Property(x => x.MaximumScore)
            //    .HasColumnType("numeric(18, 2)");

            // Устанавливаем данные
            CreateUsersAndRoles(modelBuilder, users, roles);
            //CreateGroupsAndRelations(modelBuilder, users, roles, groups);
            //CreateTeacherAndSubjectRelations(modelBuilder, users, subjects);
            //modelBuilder.Entity<LabWork>().HasData(labworks);
            //modelBuilder.Entity<LabWorkStatus>().HasData(labworkstatuses);
            modelBuilder.Entity<SubjectMapping>().HasData(subjectMappings);

            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }

        private SubjectMapping[] GetSubjectMappings()
        {
            return new SubjectMapping[]
            {
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "История России", SubjectNameCurriculum = "История России" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Управление программными проектами", SubjectNameCurriculum = "Управление программными проектами" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Самостоятельная работа студента", SubjectNameCurriculum = null },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Экономика программной инженерии", SubjectNameCurriculum = "Экономика программной инженерии" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Микропроцессорные системы", SubjectNameCurriculum = "Микропроцессорные системы" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Сопровождение программного обеспечения", SubjectNameCurriculum = "Сопровождение программного обеспечения" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Математический аhализ", SubjectNameCurriculum = "Математический анализ" },

                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Проектирование человеко-машинного интерфейса", SubjectNameCurriculum = "Проектирование человеко-машинного интерфейса" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Разработка и анализ требований , конструирование программного обеспечения", SubjectNameCurriculum = "Разработка и анализ требований, конструирование программного обеспечения" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Теория принятия решений", SubjectNameCurriculum = "Теория принятия решений" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Разработка приложений asp.net", SubjectNameCurriculum = "Разработка приложений ASP.NET" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Субд postgresql", SubjectNameCurriculum = "СУБД PostgreSQL" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Элективные дисциплины по физической культуре и спорту", SubjectNameCurriculum = "Элективные дисциплины по физической культуре и спорту" },
                new SubjectMapping { Id = Guid.NewGuid(), SubjectNameApiTable = "Тестирование программного обеспечения", SubjectNameCurriculum = "Тестирование программного обеспечения" },
            };
        }

        //private LabWorkStatus[] GetLabWorkStatuses(LabWork[] labworks, User[] users)
        //{
        //    return new LabWorkStatus[]
        //    {
        //        new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 1).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
        //        new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 2).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
        //        new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 3).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
        //        new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 4).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
        //    };
        //}

        //private LabWork[] GetLabWorks(Subject[] subjects)
        //{
        //    return new LabWork[]
        //    {
        //        new LabWork { Id = Guid.NewGuid(), Number = 1, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
        //        new LabWork { Id = Guid.NewGuid(), Number = 2, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
        //        new LabWork { Id = Guid.NewGuid(), Number = 3, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
        //        new LabWork { Id = Guid.NewGuid(), Number = 4, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
        //    };
        //}

        //private void CreateTeacherAndSubjectRelations(ModelBuilder modelBuilder, User[] users, Subject[] subjects)
        //{
        //    modelBuilder.Entity<Subject>().HasData(subjects);
        //    modelBuilder.Entity<TeacherSubject>().HasData(
        //            new TeacherSubject
        //            {
        //                UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
        //                SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id
        //            }
        //        );
        //}

        //private Subject[] GetSubjects(Group[] groups)
        //{
        //    return new Subject[]
        //    {
        //        new Subject { Id = Guid.NewGuid(), Name = "СУБД PostgreSQL", GroupId = groups.First(x => x.Name == "ДИПРБ" && x.Course == 4).Id},
        //    };

        //}

        //private void CreateGroupsAndRelations(ModelBuilder modelBuilder, User[] users, Role[] roles, Group[] groups)
        //{
        //    modelBuilder.Entity<Group>().HasData(groups);
        //    modelBuilder.Entity<UserGroup>().HasData(
        //            new UserGroup
        //            {
        //                Id = Guid.NewGuid(),
        //                UserId = users.Single(x => x.Email == "student@mail.ru").Id,
        //                GroupId = groups.Single(x => x.Name == "ДИПРБ").Id,
        //                RoleId = roles.Single(x => x.Name == "Student").Id
        //            },
        //            new UserGroup
        //            {
        //                Id = Guid.NewGuid(),
        //                UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
        //                GroupId = groups.Single(x => x.Name == "ДИИЭБ").Id,
        //                RoleId = roles.Single(x => x.Name == "Teacher").Id
        //            },
        //            new UserGroup
        //            {
        //                Id = Guid.NewGuid(),
        //                UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
        //                GroupId = groups.Single(x => x.Name == "ДИПРБ").Id,
        //                RoleId = roles.Single(x => x.Name == "Teacher").Id
        //            }
        //        );
        //}

        //private Group[] GetGroups()
        //{
        //    var groups = new Group[]
        //    {
        //        new Group { Id = Guid.NewGuid(), Name = "ДИПРБ", Course = 4, YearCreated = 2020, CurriculumExcelDocument = new byte[1] },
        //        new Group { Id = Guid.NewGuid(), Name = "ДИИЭБ", Course = 4, YearCreated = 2020, CurriculumExcelDocument = new byte[1] },
        //    };

        //    return groups;
        //}

        private void CreateUsersAndRoles(ModelBuilder modelBuilder, User[] users, Role[] roles)
        {
            modelBuilder.Entity<Role>().HasData(roles);
            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<UserRole>().HasData(
                    new UserRole
                    {
                        UserId = users.Single(x => x.Email == "admin@mail.ru").Id,
                        RoleId = roles.Single(x => x.Name == "Admin").Id
                    },
                    new UserRole
                    {
                        UserId = users.Single(x => x.Email == "student@mail.ru").Id,
                        RoleId = roles.Single(x => x.Name == "Student").Id
                    },
                    new UserRole
                    {
                        UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
                        RoleId = roles.Single(x => x.Name == "Teacher").Id
                    },
                    new UserRole
                    {
                        UserId = users.Single(x => x.Email == "teacherAdmin@mail.ru").Id,
                        RoleId = roles.Single(x => x.Name == "Teacher").Id
                    },
                    new UserRole
                    {
                        UserId = users.Single(x => x.Email == "teacherAdmin@mail.ru").Id,
                        RoleId = roles.Single(x => x.Name == "Admin").Id
                    }
                );
        }

        private User[] GetUsers()
        {
            var hasher = new PasswordHasher();

            // Первый пользователь (админ)
            var request = new UserDto { Email = "admin@mail.ru", Password = "12345" };
            var user1 = GetUser(request, hasher);

            // Второй пользователь (студент)
            request = new UserDto { Email = "student@mail.ru", Password = "12345" };
            var user2 = GetUser(request, hasher);

            // Третий пользователь (преподаватель)
            request = new UserDto { Email = "teacher@mail.ru", Password = "12345" };
            var user3 = GetUser(request, hasher);

            // Четвертый пользователь (преподаватель и администратор)
            request = new UserDto { Email = "teacherAdmin@mail.ru", Password = "12345" };
            var user4 = GetUser(request, hasher);

            return new User[] { user1, user2, user3, user4 };
        }

        private static User GetUser(UserDto request, IPasswordHasher passwordHasher)
        {
            passwordHasher.CreateHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User(request.Email, passwordHash, passwordSalt);
            user.Id = Guid.NewGuid();

            return user;
        }

        private static Role[] GetRoles()
        {
            const string studentRoleName = "Student";
            const string teacherRoleName = "Teacher";
            const string adminRoleName = "Admin";

            var studentRole = new Role { Id = Guid.NewGuid(), Name = studentRoleName };
            var teacherRole = new Role { Id = Guid.NewGuid(), Name = teacherRoleName };
            var adminRole = new Role { Id = Guid.NewGuid(), Name = adminRoleName };

            return new Role[] { studentRole, teacherRole, adminRole };
        }
    }
}
