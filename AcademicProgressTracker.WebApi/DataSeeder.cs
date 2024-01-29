using AcademicProgressTracker.Application.Common.DTOs;
using AcademicProgressTracker.Application.Common.Interfaces;
using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence;

namespace AcademicProgressTracker.WebApi
{
    public static class DataSeeder
    {
        public static void Seed(this IHost host)
        {
            using var scope = host.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<AcademicProgressDataContext>();

            if (!context.Roles.Any())
            {
                //IPasswordHasher hasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

                var roles = AllRoles();
                //var groups = new Group[] { new Group { Id = Guid.NewGuid(), Name = "ДИПР", Course = 4, YearCreated = 2020 } };

                //// Первый пользователь (админ)
                //var request = new UserDto { Email = "gorstsergei@mail.ru", Password = "12345" };
                //var user1 = GetUser(request, hasher);
                //user1.RoleId = roles[2].Id;
                //user1.Id = Guid.NewGuid();
                //var administrator = new Administrator { Id = Guid.NewGuid(), Name = "Горст Сергей Германович", UserId = user1.Id };

                //// Второй пользователь (студент)
                //request = new UserDto { Email = "student@mail.ru", Password = "12345" };
                //var user2 = GetUser(request, hasher);
                //user2.RoleId = roles[0].Id;
                //user2.Id = Guid.NewGuid();
                //var student = new Student { Id = Guid.NewGuid(), Name = "Студент Студентович", UserId = user2.Id, GroupId = groups[0].Id };

                //// Третий пользователь (преподаватель)
                //request = new UserDto { Email = "teacher1@mail.ru", Password = "12345" };
                //var user3 = GetUser(request, hasher);
                //user3.RoleId = roles[1].Id;
                //user3.Id = Guid.NewGuid();
                //var teacher1 = new Teacher { Id = Guid.NewGuid(), Name = "Белов Сергей Валерьевич", UserId = user3.Id };

                //// Четвертый пользователь (преподаватель)
                //request = new UserDto { Email = "teacher2@mail.ru", Password = "12345" };
                //var user4 = GetUser(request, hasher);
                //user4.RoleId = roles[1].Id;
                //user4.Id = Guid.NewGuid();
                //var teacher2 = new Teacher { Id = Guid.NewGuid(), Name = "Куркурин Николай Дмитриевич", UserId = user4.Id };

                // ----------------------------------- //

                //context.Groups.AddRange(groups);
                context.Roles.AddRange(roles);
                //context.Users.AddRange(user1, user2, user3, user4);
                //context.Administrators.Add(administrator);
                //context.Students.Add(student);
                //context.Teachers.AddRange(teacher1, teacher2);

                //teacher1.Groups.Add(groups[0]);
                //teacher2.Groups.Add(groups[0]);

                context.SaveChanges();
            }
        }

        private static User GetUser(UserDto request, IPasswordHasher passwordHasher)
        {
            passwordHasher.CreateHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            return user;
        }

        private static Role[] AllRoles()
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
