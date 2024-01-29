using AcademicProgressTracker.Persistence.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace AcademicProgressTracker.Persistence.EntityTypeConfigurations
{
    internal class UserGroupConfiguration : IEntityTypeConfiguration<UserGroup>
    {
        private readonly Guid _studentRoleId;

        public UserGroupConfiguration(Guid studentRoleId)
        {
            _studentRoleId = studentRoleId;
        }

        // Данная конфигурация устанавливает правило, что у студента может быть только одна группа
        public void Configure(EntityTypeBuilder<UserGroup> builder)
        {
            builder
                .HasIndex(ug => new { ug.UserId, ug.RoleId })
                .IsUnique()
                .HasFilter($"\"RoleId\" = '{_studentRoleId}'");
            // P.S. HasFilter работает только с синтаксисом SQL, поэтому берем RoleId в кавычки - "RoleId"
        }
    }
}
