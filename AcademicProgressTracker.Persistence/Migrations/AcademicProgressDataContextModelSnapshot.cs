﻿// <auto-generated />
using System;
using AcademicProgressTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AcademicProgressTracker.Persistence.Migrations
{
    [DbContext(typeof(AcademicProgressDataContext))]
    partial class AcademicProgressDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseSerialColumns(modelBuilder);

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Course")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("YearCreated")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("62264dfc-8eab-43f2-bffd-a4f165b4dace"),
                            Course = 4,
                            Name = "ДИПРБ",
                            YearCreated = 2020
                        },
                        new
                        {
                            Id = new Guid("8e5a260d-1a98-4d0c-9683-75a418b1b560"),
                            Course = 4,
                            Name = "ДИИЭБ",
                            YearCreated = 2020
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<float>("MaximumScore")
                        .HasColumnType("real");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("LabWorks");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWorkStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LabWorkId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LabWorkId");

                    b.HasIndex("UserId");

                    b.ToTable("LabWorkStatuses");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3814c6e4-5cca-4681-8bd5-998aef007642"),
                            Name = "Student"
                        },
                        new
                        {
                            Id = new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"),
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = new Guid("472e417a-2f85-4dc4-8028-da121e1746eb"),
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Subject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Semester")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1529e062-f8d8-4d2f-aec8-e348b64080e0"),
                            Email = "admin@mail.ru",
                            PasswordHash = new byte[] { 151, 94, 212, 30, 156, 124, 74, 139, 34, 32, 189, 202, 11, 183, 225, 24, 8, 113, 196, 192, 12, 62, 11, 118, 148, 54, 70, 46, 82, 197, 191, 188, 37, 102, 234, 28, 202, 244, 206, 18, 255, 68, 228, 123, 106, 247, 114, 125, 37, 77, 102, 40, 211, 69, 130, 171, 231, 80, 243, 244, 30, 5, 180, 33 },
                            PasswordSalt = new byte[] { 112, 221, 30, 81, 248, 138, 186, 222, 44, 229, 168, 82, 183, 117, 224, 36, 238, 92, 173, 195, 225, 239, 131, 34, 66, 34, 45, 120, 168, 110, 176, 53, 181, 207, 157, 216, 127, 1, 130, 115, 0, 186, 93, 154, 50, 3, 33, 32, 67, 128, 159, 217, 229, 129, 103, 91, 233, 158, 220, 254, 232, 149, 150, 236, 70, 27, 175, 29, 128, 21, 195, 48, 46, 116, 39, 219, 120, 21, 182, 110, 191, 198, 237, 92, 237, 199, 40, 161, 195, 15, 128, 26, 84, 231, 214, 87, 210, 76, 133, 168, 100, 250, 91, 123, 188, 245, 227, 29, 161, 168, 174, 87, 59, 13, 132, 201, 80, 45, 136, 45, 245, 82, 83, 163, 69, 45, 248, 30 },
                            RefreshToken = "",
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("7a729276-17cc-4f95-94c1-a4c5315b203f"),
                            Email = "student@mail.ru",
                            PasswordHash = new byte[] { 250, 187, 49, 188, 160, 77, 112, 103, 44, 48, 183, 48, 202, 65, 73, 119, 37, 172, 188, 107, 7, 230, 120, 34, 116, 12, 234, 28, 33, 227, 25, 197, 34, 89, 124, 98, 16, 81, 41, 2, 97, 242, 88, 215, 160, 75, 149, 27, 236, 136, 235, 76, 120, 156, 241, 98, 74, 102, 72, 10, 236, 164, 223, 146 },
                            PasswordSalt = new byte[] { 5, 3, 170, 62, 125, 28, 167, 82, 94, 222, 10, 230, 140, 248, 68, 28, 28, 109, 105, 32, 112, 118, 232, 183, 152, 6, 204, 226, 255, 213, 46, 155, 13, 218, 15, 252, 30, 214, 144, 44, 239, 159, 3, 137, 170, 28, 138, 134, 158, 93, 205, 236, 152, 246, 124, 70, 238, 196, 31, 127, 51, 116, 40, 235, 10, 228, 167, 250, 100, 251, 14, 185, 235, 8, 139, 92, 85, 166, 100, 242, 192, 146, 113, 75, 48, 184, 169, 219, 93, 1, 250, 115, 106, 228, 44, 216, 183, 62, 16, 158, 189, 111, 224, 131, 120, 140, 74, 101, 220, 142, 150, 65, 183, 179, 201, 176, 26, 209, 142, 50, 16, 229, 9, 171, 114, 59, 188, 221 },
                            RefreshToken = "",
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("01179748-f8ab-4e8e-9406-953a16a28486"),
                            Email = "teacher@mail.ru",
                            PasswordHash = new byte[] { 137, 42, 9, 21, 129, 3, 154, 50, 197, 212, 59, 13, 182, 151, 202, 39, 39, 99, 237, 89, 151, 38, 24, 124, 247, 176, 169, 55, 103, 191, 136, 157, 93, 108, 252, 127, 113, 85, 40, 33, 174, 194, 183, 199, 32, 115, 186, 239, 176, 202, 0, 85, 204, 218, 91, 95, 168, 188, 142, 141, 119, 34, 69, 229 },
                            PasswordSalt = new byte[] { 93, 227, 166, 52, 14, 47, 106, 18, 95, 145, 191, 189, 114, 184, 162, 157, 167, 81, 135, 213, 35, 226, 105, 81, 29, 55, 146, 190, 171, 146, 254, 161, 5, 3, 243, 57, 177, 126, 77, 194, 82, 254, 222, 118, 77, 24, 44, 46, 163, 196, 87, 213, 119, 236, 75, 192, 52, 81, 158, 231, 75, 174, 9, 15, 49, 84, 251, 34, 104, 198, 246, 88, 132, 252, 247, 178, 66, 140, 222, 33, 214, 78, 103, 248, 14, 219, 37, 196, 167, 65, 146, 8, 53, 222, 98, 146, 184, 160, 146, 248, 10, 45, 120, 22, 165, 73, 249, 58, 155, 237, 133, 241, 33, 5, 134, 175, 124, 150, 57, 145, 96, 21, 156, 104, 217, 211, 210, 116 },
                            RefreshToken = "",
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("4da62e83-b06d-4a96-af7a-a4fb9f4a6987"),
                            Email = "teacherAdmin@mail.ru",
                            PasswordHash = new byte[] { 34, 32, 34, 218, 153, 44, 191, 216, 245, 164, 177, 106, 91, 118, 172, 13, 84, 67, 4, 116, 159, 69, 235, 32, 164, 154, 220, 16, 187, 51, 63, 188, 22, 99, 199, 70, 133, 103, 205, 17, 150, 138, 246, 126, 116, 201, 164, 16, 30, 112, 93, 42, 86, 155, 17, 26, 75, 95, 93, 220, 71, 170, 99, 29 },
                            PasswordSalt = new byte[] { 189, 12, 107, 62, 200, 77, 85, 224, 61, 172, 231, 158, 238, 236, 118, 153, 226, 124, 18, 237, 49, 161, 43, 128, 243, 234, 45, 163, 65, 38, 183, 133, 207, 31, 9, 246, 242, 183, 202, 6, 34, 128, 96, 68, 20, 52, 220, 153, 28, 226, 221, 61, 161, 91, 14, 120, 134, 98, 140, 248, 177, 120, 86, 80, 246, 160, 20, 140, 169, 240, 218, 87, 229, 37, 2, 43, 51, 26, 60, 128, 61, 164, 170, 192, 155, 217, 109, 47, 89, 148, 114, 153, 13, 45, 240, 64, 42, 163, 250, 250, 101, 104, 161, 250, 44, 22, 70, 177, 254, 16, 212, 189, 163, 148, 255, 140, 160, 240, 44, 202, 220, 186, 161, 4, 147, 38, 248, 109 },
                            RefreshToken = "",
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.UserGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId", "RoleId")
                        .IsUnique()
                        .HasFilter("\"RoleId\" = '3814c6e4-5cca-4681-8bd5-998aef007642'");

                    b.ToTable("UserGroup");

                    b.HasData(
                        new
                        {
                            Id = new Guid("56abbbde-3e74-41be-93e5-e81fdf815129"),
                            GroupId = new Guid("62264dfc-8eab-43f2-bffd-a4f165b4dace"),
                            RoleId = new Guid("3814c6e4-5cca-4681-8bd5-998aef007642"),
                            UserId = new Guid("7a729276-17cc-4f95-94c1-a4c5315b203f")
                        },
                        new
                        {
                            Id = new Guid("53b3f9a7-1223-4055-9970-5837667b2704"),
                            GroupId = new Guid("8e5a260d-1a98-4d0c-9683-75a418b1b560"),
                            RoleId = new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"),
                            UserId = new Guid("01179748-f8ab-4e8e-9406-953a16a28486")
                        },
                        new
                        {
                            Id = new Guid("14d30da8-386d-49f1-bc51-6ddccafe541d"),
                            GroupId = new Guid("62264dfc-8eab-43f2-bffd-a4f165b4dace"),
                            RoleId = new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"),
                            UserId = new Guid("01179748-f8ab-4e8e-9406-953a16a28486")
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.UserRole", b =>
                {
                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRole", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("472e417a-2f85-4dc4-8028-da121e1746eb"),
                            UserId = new Guid("1529e062-f8d8-4d2f-aec8-e348b64080e0")
                        },
                        new
                        {
                            RoleId = new Guid("3814c6e4-5cca-4681-8bd5-998aef007642"),
                            UserId = new Guid("7a729276-17cc-4f95-94c1-a4c5315b203f")
                        },
                        new
                        {
                            RoleId = new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"),
                            UserId = new Guid("01179748-f8ab-4e8e-9406-953a16a28486")
                        },
                        new
                        {
                            RoleId = new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"),
                            UserId = new Guid("4da62e83-b06d-4a96-af7a-a4fb9f4a6987")
                        },
                        new
                        {
                            RoleId = new Guid("472e417a-2f85-4dc4-8028-da121e1746eb"),
                            UserId = new Guid("4da62e83-b06d-4a96-af7a-a4fb9f4a6987")
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWork", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWorkStatus", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.LabWork", "LabWork")
                        .WithMany()
                        .HasForeignKey("LabWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicProgressTracker.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LabWork");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Subject", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.UserGroup", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicProgressTracker.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicProgressTracker.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.UserRole", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicProgressTracker.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
