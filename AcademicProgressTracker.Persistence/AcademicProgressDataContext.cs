using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence.EntityTypeConfigurations;
using AcademicProgressTracker.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Reflection.Emit;

namespace AcademicProgressTracker.Persistence
{
    public class AcademicProgressDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<TeacherSubject> TeacherSubject { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<LabWork> LabWorks { get; set; }
        public DbSet<LabWorkStatus> LabWorkStatuses { get; set; }

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
            var groups = GetGroups();
            var subjects = GetSubjects(groups);
            var labworks = GetLabWorks(subjects);
            var labworkstatuses = GetLabWorkStatuses(labworks, users);

            // Применяем конфигурации
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupConfiguration(roles.Single(x => x.Name == "Student").Id));

            // Устанавливаем данные
            CreateUsersAndRoles(modelBuilder, users, roles);
            CreateGroupsAndRelations(modelBuilder, users, roles, groups);
            CreateTeacherAndSubjectRelations(modelBuilder, users, subjects);
            modelBuilder.Entity<LabWork>().HasData(labworks);
            modelBuilder.Entity<LabWorkStatus>().HasData(labworkstatuses);

            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }

        private LabWorkStatus[] GetLabWorkStatuses(LabWork[] labworks, User[] users)
        {
            return new LabWorkStatus[]
            {
                new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 1).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
                new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 2).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
                new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 3).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
                new LabWorkStatus { Id = Guid.NewGuid(), IsCompleted = false, LabWorkId = labworks.Single(x => x.Number == 4).Id, UserId = users.Single(x => x.Email == "student@mail.ru").Id },
            };
        }

        private LabWork[] GetLabWorks(Subject[] subjects)
        {
            return new LabWork[]
            {
                new LabWork { Id = Guid.NewGuid(), Number = 1, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
                new LabWork { Id = Guid.NewGuid(), Number = 2, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
                new LabWork { Id = Guid.NewGuid(), Number = 3, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
                new LabWork { Id = Guid.NewGuid(), Number = 4, MaximumScore = 10, SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id },
            };
        }

        private void CreateTeacherAndSubjectRelations(ModelBuilder modelBuilder, User[] users, Subject[] subjects)
        {
            modelBuilder.Entity<Subject>().HasData(subjects);
            modelBuilder.Entity<TeacherSubject>().HasData(
                    new TeacherSubject
                    {
                        UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
                        SubjectId = subjects.Single(x => x.Name == "СУБД PostgreSQL").Id
                    }
                );
        }

        private Subject[] GetSubjects(Group[] groups)
        {
            return new Subject[]
            {
                new Subject { Id = Guid.NewGuid(), Name = "СУБД PostgreSQL", GroupId = groups.First(x => x.Name == "ДИПРБ" && x.Course == 4).Id},
            };

        }

        private void CreateGroupsAndRelations(ModelBuilder modelBuilder, User[] users, Role[] roles, Group[] groups)
        {
            modelBuilder.Entity<Group>().HasData(groups);
            modelBuilder.Entity<UserGroup>().HasData(
                    new UserGroup
                    {
                        Id = Guid.NewGuid(),
                        UserId = users.Single(x => x.Email == "student@mail.ru").Id,
                        GroupId = groups.Single(x => x.Name == "ДИПРБ").Id,
                        RoleId = roles.Single(x => x.Name == "Student").Id
                    },
                    new UserGroup
                    {
                        Id = Guid.NewGuid(),
                        UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
                        GroupId = groups.Single(x => x.Name == "ДИИЭБ").Id,
                        RoleId = roles.Single(x => x.Name == "Teacher").Id
                    },
                    new UserGroup
                    {
                        Id = Guid.NewGuid(),
                        UserId = users.Single(x => x.Email == "teacher@mail.ru").Id,
                        GroupId = groups.Single(x => x.Name == "ДИПРБ").Id,
                        RoleId = roles.Single(x => x.Name == "Teacher").Id
                    }
                );
        }

        private Group[] GetGroups()
        {
            var groups = new Group[]
            {
                new Group { Id = Guid.NewGuid(), Name = "ДИПРБ", Course = 4, YearCreated = 2020 },
                new Group { Id = Guid.NewGuid(), Name = "ДИИЭБ", Course = 4, YearCreated = 2020 },
            };

            return groups;
        }

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
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

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
