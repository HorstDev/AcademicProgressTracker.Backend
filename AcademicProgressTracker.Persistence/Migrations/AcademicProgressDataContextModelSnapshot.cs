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

                    b.Property<byte[]>("CurriculumExcelDocument")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<decimal>("Score")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("LabWorks");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWorkUserStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDone")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LabWorkId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LabWorkId");

                    b.HasIndex("UserId");

                    b.ToTable("LabWorkUserStatuses");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)");

                    b.Property<DateTime?>("End")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SubjectId");

                    b.ToTable("Lessons");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Lesson");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LessonUserStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVisited")
                        .HasColumnType("boolean");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("LessonUserStatuses");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Profiles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Profile");

                    b.UseTphMappingStrategy();
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
                            Id = new Guid("4216f7f2-bccc-44c7-a7cc-08100f9b7edc"),
                            Name = "Student"
                        },
                        new
                        {
                            Id = new Guid("483d6fba-ac76-4141-8866-7fc2a0ae38c1"),
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = new Guid("adab11cd-c92f-4b29-8a0f-1fed6e4806df"),
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
                            Id = new Guid("16f65ca9-0807-4e10-a9b0-faa38ed81d7e"),
                            Email = "admin@mail.ru",
                            PasswordHash = new byte[] { 43, 88, 227, 16, 174, 106, 20, 232, 171, 155, 107, 103, 84, 50, 171, 120, 164, 183, 111, 247, 88, 173, 216, 251, 231, 156, 22, 184, 116, 74, 5, 217, 88, 166, 74, 152, 167, 103, 128, 110, 219, 193, 41, 37, 63, 66, 249, 40, 232, 246, 212, 218, 37, 221, 255, 73, 11, 191, 49, 204, 39, 42, 228, 232 },
                            PasswordSalt = new byte[] { 202, 2, 224, 141, 47, 250, 39, 144, 87, 117, 209, 37, 153, 99, 175, 22, 166, 176, 171, 209, 134, 57, 58, 120, 254, 93, 92, 161, 81, 10, 196, 119, 150, 62, 214, 44, 62, 240, 47, 81, 211, 181, 23, 158, 72, 108, 3, 234, 35, 139, 187, 58, 169, 246, 167, 157, 157, 250, 101, 121, 20, 55, 79, 216, 90, 46, 80, 227, 119, 0, 47, 253, 129, 224, 31, 187, 7, 5, 215, 7, 4, 131, 131, 241, 207, 219, 130, 239, 165, 46, 98, 0, 216, 242, 16, 162, 129, 215, 162, 110, 10, 82, 130, 3, 200, 248, 159, 217, 217, 62, 237, 178, 125, 133, 62, 74, 158, 111, 148, 244, 20, 228, 127, 231, 102, 89, 127, 28 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("5a35e590-ccec-4572-a4a8-016922e2dc77"),
                            Email = "student@mail.ru",
                            PasswordHash = new byte[] { 48, 152, 4, 27, 183, 232, 89, 42, 215, 58, 110, 226, 143, 16, 126, 74, 107, 94, 139, 207, 248, 209, 144, 239, 34, 5, 253, 243, 143, 167, 242, 213, 92, 30, 166, 56, 136, 29, 237, 63, 21, 222, 243, 48, 161, 0, 64, 228, 158, 168, 125, 46, 127, 198, 37, 122, 132, 5, 170, 16, 177, 217, 154, 8 },
                            PasswordSalt = new byte[] { 255, 209, 191, 217, 118, 156, 20, 103, 105, 101, 1, 8, 206, 36, 93, 190, 59, 233, 65, 84, 237, 248, 227, 65, 239, 169, 78, 192, 172, 143, 119, 201, 163, 29, 56, 8, 100, 205, 109, 181, 99, 163, 155, 232, 253, 130, 61, 207, 105, 60, 65, 26, 108, 99, 81, 153, 222, 204, 16, 197, 183, 75, 114, 221, 28, 131, 100, 254, 73, 56, 113, 209, 245, 91, 198, 85, 248, 204, 215, 157, 69, 39, 79, 168, 212, 145, 107, 204, 85, 174, 61, 75, 76, 150, 60, 240, 97, 37, 3, 112, 214, 35, 124, 16, 131, 160, 160, 82, 250, 26, 234, 90, 203, 120, 219, 138, 116, 142, 161, 34, 40, 168, 95, 17, 207, 43, 15, 54 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("894933e0-f29a-4c8a-beb1-ce252db6f26f"),
                            Email = "teacher@mail.ru",
                            PasswordHash = new byte[] { 84, 4, 228, 226, 162, 227, 69, 155, 191, 178, 19, 166, 164, 103, 153, 81, 175, 137, 251, 117, 31, 198, 185, 65, 169, 33, 91, 96, 152, 194, 119, 202, 118, 39, 237, 74, 240, 170, 107, 203, 0, 217, 2, 144, 55, 104, 64, 189, 165, 160, 202, 33, 231, 248, 55, 73, 10, 65, 36, 144, 50, 84, 255, 114 },
                            PasswordSalt = new byte[] { 24, 48, 85, 41, 157, 28, 69, 239, 15, 228, 71, 174, 197, 146, 236, 37, 51, 120, 226, 134, 114, 148, 150, 178, 94, 113, 204, 12, 238, 25, 40, 158, 129, 163, 83, 66, 178, 132, 12, 46, 142, 101, 216, 31, 199, 117, 35, 248, 64, 186, 229, 251, 26, 248, 191, 196, 199, 204, 166, 67, 104, 193, 195, 15, 149, 174, 142, 133, 251, 244, 13, 15, 49, 92, 212, 111, 47, 42, 116, 138, 111, 126, 111, 232, 135, 180, 103, 28, 138, 15, 85, 83, 159, 189, 246, 11, 137, 226, 36, 110, 180, 165, 209, 248, 247, 151, 179, 199, 95, 223, 169, 136, 18, 14, 242, 201, 62, 127, 0, 126, 237, 4, 190, 74, 130, 131, 244, 220 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("4ceacfcc-bb29-4bdf-8164-58fbc3f0fc8d"),
                            Email = "teacherAdmin@mail.ru",
                            PasswordHash = new byte[] { 222, 114, 190, 198, 243, 207, 254, 105, 160, 120, 26, 15, 88, 36, 135, 40, 182, 7, 200, 84, 23, 167, 48, 85, 125, 30, 120, 133, 167, 56, 166, 66, 114, 166, 190, 40, 200, 136, 160, 227, 75, 84, 235, 129, 230, 230, 172, 151, 182, 35, 175, 221, 217, 206, 49, 223, 226, 119, 98, 191, 37, 227, 53, 189 },
                            PasswordSalt = new byte[] { 14, 5, 184, 124, 96, 45, 240, 51, 33, 188, 9, 1, 68, 100, 28, 171, 70, 193, 116, 59, 119, 19, 119, 113, 245, 204, 229, 169, 176, 82, 219, 41, 25, 152, 197, 242, 3, 228, 209, 65, 94, 207, 100, 172, 207, 155, 244, 27, 12, 12, 67, 106, 127, 214, 96, 5, 167, 172, 221, 3, 101, 70, 101, 119, 66, 141, 244, 215, 80, 114, 73, 28, 144, 163, 18, 113, 29, 108, 13, 162, 0, 251, 130, 76, 130, 148, 38, 250, 72, 127, 190, 146, 201, 114, 140, 123, 150, 205, 126, 225, 136, 21, 228, 190, 93, 164, 230, 150, 145, 249, 173, 33, 251, 205, 29, 4, 248, 67, 67, 72, 239, 222, 88, 51, 244, 36, 67, 160 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.SubjectMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("SubjectNameApiTable")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SubjectNameCurriculum")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SubjectMappings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6e73e9a7-d190-482a-bfc8-511f3c535905"),
                            SubjectNameApiTable = "История россии",
                            SubjectNameCurriculum = "История России"
                        },
                        new
                        {
                            Id = new Guid("a1273ec5-69c2-4556-b6a1-4009aeef4367"),
                            SubjectNameApiTable = "Управление программными проектами",
                            SubjectNameCurriculum = "Управление программными проектами"
                        },
                        new
                        {
                            Id = new Guid("35513905-d739-4c6b-b94d-a4224e385cf3"),
                            SubjectNameApiTable = "Самостоятельная работа студента"
                        },
                        new
                        {
                            Id = new Guid("444e3957-439e-4ec2-be54-286e872d54fa"),
                            SubjectNameApiTable = "Экономика программной инженерии",
                            SubjectNameCurriculum = "Экономика программной инженерии"
                        },
                        new
                        {
                            Id = new Guid("50c1b0be-dc01-4eb4-aeaf-b8ca4ba4b916"),
                            SubjectNameApiTable = "Микропроцессорные системы",
                            SubjectNameCurriculum = "Микропроцессорные системы"
                        },
                        new
                        {
                            Id = new Guid("181e3464-7648-4b62-88d4-de8c020502e1"),
                            SubjectNameApiTable = "Сопровождение программного обеспечения",
                            SubjectNameCurriculum = "Сопровождение программного обеспечения"
                        },
                        new
                        {
                            Id = new Guid("06859239-ddee-46dc-9848-b4234a5d6a9a"),
                            SubjectNameApiTable = "Математический аhализ",
                            SubjectNameCurriculum = "Математический анализ"
                        },
                        new
                        {
                            Id = new Guid("5b3d60d3-49fe-451f-bafb-4213370c5861"),
                            SubjectNameApiTable = "Проектирование человеко-машинного интерфейса",
                            SubjectNameCurriculum = "Проектирование человеко-машинного интерфейса"
                        },
                        new
                        {
                            Id = new Guid("7ac5066a-afd7-41b6-94e9-30b0548ce082"),
                            SubjectNameApiTable = "Разработка и анализ требований , конструирование программного обеспечения",
                            SubjectNameCurriculum = "Разработка и анализ требований, конструирование программного обеспечения"
                        },
                        new
                        {
                            Id = new Guid("3e002d6b-9d1b-464c-b9da-4ea84c6addce"),
                            SubjectNameApiTable = "Теория принятия решений",
                            SubjectNameCurriculum = "Теория принятия решений"
                        },
                        new
                        {
                            Id = new Guid("f421386d-c9e0-4943-b7e7-c941c21d6a3f"),
                            SubjectNameApiTable = "Разработка приложений asp.net",
                            SubjectNameCurriculum = "Разработка приложений ASP.NET"
                        },
                        new
                        {
                            Id = new Guid("c778cd84-d6ec-4469-b067-9ec2b23e00c6"),
                            SubjectNameApiTable = "Субд postgresql",
                            SubjectNameCurriculum = "СУБД PostgreSQL"
                        },
                        new
                        {
                            Id = new Guid("bf253128-2130-41e0-bd30-4c3a1b5111bf"),
                            SubjectNameApiTable = "Элективные дисциплины по физической культуре и спорту",
                            SubjectNameCurriculum = "Элективные дисциплины по физической культуре и спорту"
                        },
                        new
                        {
                            Id = new Guid("c9f24451-68e1-4498-a234-b18050bad1e4"),
                            SubjectNameApiTable = "Тестирование программного обеспечения",
                            SubjectNameCurriculum = "Тестирование программного обеспечения"
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.TeacherSubject", b =>
                {
                    b.Property<Guid>("SubjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("SubjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TeacherSubject", (string)null);
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
                        .HasFilter("\"RoleId\" = '4216f7f2-bccc-44c7-a7cc-08100f9b7edc'");

                    b.ToTable("UserGroup");
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
                            RoleId = new Guid("adab11cd-c92f-4b29-8a0f-1fed6e4806df"),
                            UserId = new Guid("16f65ca9-0807-4e10-a9b0-faa38ed81d7e")
                        },
                        new
                        {
                            RoleId = new Guid("4216f7f2-bccc-44c7-a7cc-08100f9b7edc"),
                            UserId = new Guid("5a35e590-ccec-4572-a4a8-016922e2dc77")
                        },
                        new
                        {
                            RoleId = new Guid("483d6fba-ac76-4141-8866-7fc2a0ae38c1"),
                            UserId = new Guid("894933e0-f29a-4c8a-beb1-ce252db6f26f")
                        },
                        new
                        {
                            RoleId = new Guid("483d6fba-ac76-4141-8866-7fc2a0ae38c1"),
                            UserId = new Guid("4ceacfcc-bb29-4bdf-8164-58fbc3f0fc8d")
                        },
                        new
                        {
                            RoleId = new Guid("adab11cd-c92f-4b29-8a0f-1fed6e4806df"),
                            UserId = new Guid("4ceacfcc-bb29-4bdf-8164-58fbc3f0fc8d")
                        });
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabLesson", b =>
                {
                    b.HasBaseType("AcademicProgressTracker.Domain.Entities.Lesson");

                    b.Property<Guid?>("LabWorkId")
                        .HasColumnType("uuid");

                    b.HasIndex("LabWorkId");

                    b.HasDiscriminator().HasValue("LabLesson");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.TeacherProfile", b =>
                {
                    b.HasBaseType("AcademicProgressTracker.Domain.Entities.Profile");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("TeacherProfile");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWorkUserStatus", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.LabWork", "LabWork")
                        .WithMany("UserStatuses")
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

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Lesson", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Subject", "Subject")
                        .WithMany("Lessons")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LessonUserStatus", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Lesson", "Lesson")
                        .WithMany("UserStatuses")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicProgressTracker.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Profile", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.User", "User")
                        .WithMany("Profiles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Subject", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Group", "Group")
                        .WithMany("Subjects")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("AcademicProgressTracker.Persistence.Models.TeacherSubject", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AcademicProgressTracker.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Subject");

                    b.Navigation("User");
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

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabLesson", b =>
                {
                    b.HasOne("AcademicProgressTracker.Domain.Entities.LabWork", "LabWork")
                        .WithMany("Lessons")
                        .HasForeignKey("LabWorkId");

                    b.Navigation("LabWork");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Group", b =>
                {
                    b.Navigation("Subjects");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LabWork", b =>
                {
                    b.Navigation("Lessons");

                    b.Navigation("UserStatuses");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Lesson", b =>
                {
                    b.Navigation("UserStatuses");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.Subject", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.User", b =>
                {
                    b.Navigation("Profiles");
                });
#pragma warning restore 612, 618
        }
    }
}
