using AcademicProgressTracker.Domain.Entities;
using AcademicProgressTracker.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace AcademicProgressTracker.Persistence.EntityTypeConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasMany(right => right.Roles)
                .WithMany(left => left.Users)
                .UsingEntity<UserRole>(
                    right => right.HasOne(e => e.Role).WithMany().HasForeignKey(e => e.RoleId),
                    left => left.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId),
                    join => join.ToTable("UserRole")
                );

            builder
                .HasMany(right => right.Subjects)
                .WithMany(left => left.Users)
                .UsingEntity<TeacherSubject>(
                    right => right.HasOne(e => e.Subject).WithMany().HasForeignKey(e => e.SubjectId),
                    left => left.HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId),
                    join => join.ToTable("TeacherSubject")
                );
        }
    }
}
