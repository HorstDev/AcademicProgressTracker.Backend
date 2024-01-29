using AcademicProgressTracker.Application.Auth;
using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence.EntityTypeConfigurations;
using AcademicProgressTracker.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AcademicProgressTracker.Persistence
{
    public class AcademicProgressDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserGroup> UserGroup { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
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

            // Применяем конфигурации
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupConfiguration(roles.Single(x => x.Name == "Student").Id));

            // Устанавливаем данные
            CreateUsersAndRoles(modelBuilder, users, roles);

            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
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
