using AcademicProgressTracker.Domain;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.Persistence
{
    public class AcademicProgressDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Administrator> Administrators { get; set; }
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
            modelBuilder.Entity<Role>().HasData(AllRoles());

            modelBuilder.UseSerialColumns();
            base.OnModelCreating(modelBuilder);
        }

        private Role[] AllRoles()
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
