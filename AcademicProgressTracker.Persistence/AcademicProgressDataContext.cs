using AcademicProgressTracker.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicProgressTracker.Persistence
{
    public class AcademicProgressDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

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
