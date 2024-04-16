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

                    b.Property<DateTime>("DateTimeOfUpdateDependenciesFromServer")
                        .HasColumnType("timestamp without time zone");

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
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsStarted")
                        .HasColumnType("boolean");

                    b.Property<int>("Number")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RealEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("RealStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("Start")
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

                    b.Property<decimal>("Score")
                        .HasColumnType("numeric");

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

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

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
                            Id = new Guid("22c95dbf-735a-434b-b1be-437445b4dd2e"),
                            Name = "Student"
                        },
                        new
                        {
                            Id = new Guid("7102ba69-f7b6-42e4-abc1-fe48aa08cf12"),
                            Name = "Teacher"
                        },
                        new
                        {
                            Id = new Guid("cae29eb6-e185-4a06-a83d-0ed776daab40"),
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
                            Id = new Guid("9c9d1d90-5a6a-4882-816d-f3b8a3fc528c"),
                            Email = "admin@mail.ru",
                            PasswordHash = new byte[] { 167, 77, 132, 138, 82, 46, 169, 84, 228, 142, 84, 48, 236, 156, 64, 18, 10, 104, 226, 1, 175, 15, 1, 24, 226, 154, 93, 213, 92, 138, 141, 129, 62, 182, 120, 254, 189, 63, 64, 140, 163, 99, 19, 117, 232, 15, 37, 231, 93, 239, 22, 206, 218, 117, 172, 195, 58, 148, 113, 210, 53, 18, 218, 216 },
                            PasswordSalt = new byte[] { 161, 11, 25, 209, 163, 112, 69, 43, 39, 187, 186, 31, 235, 35, 235, 197, 156, 134, 76, 129, 47, 74, 124, 43, 182, 196, 11, 178, 112, 21, 57, 201, 167, 31, 58, 54, 213, 66, 177, 234, 123, 141, 147, 34, 237, 17, 18, 137, 186, 172, 109, 91, 115, 45, 29, 132, 11, 146, 201, 213, 156, 149, 213, 33, 189, 49, 49, 235, 245, 75, 164, 248, 52, 30, 128, 235, 99, 136, 219, 4, 62, 140, 252, 69, 28, 88, 62, 216, 115, 70, 169, 196, 65, 249, 75, 124, 141, 93, 199, 134, 209, 255, 129, 50, 71, 30, 68, 248, 6, 247, 108, 2, 96, 223, 88, 26, 166, 164, 138, 235, 148, 229, 158, 206, 87, 160, 211, 201 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("2a68d924-7f89-4e89-9488-66b40dcb2226"),
                            Email = "student@mail.ru",
                            PasswordHash = new byte[] { 38, 16, 210, 190, 116, 147, 180, 61, 239, 199, 161, 90, 243, 119, 21, 249, 112, 104, 187, 79, 1, 106, 248, 228, 160, 118, 0, 227, 44, 175, 139, 82, 251, 222, 16, 28, 96, 201, 154, 44, 191, 0, 6, 209, 116, 242, 57, 255, 40, 87, 8, 193, 156, 19, 197, 30, 185, 111, 30, 164, 119, 75, 228, 240 },
                            PasswordSalt = new byte[] { 243, 54, 166, 211, 180, 47, 92, 26, 86, 248, 29, 20, 125, 220, 6, 1, 249, 131, 192, 54, 161, 237, 214, 177, 179, 219, 164, 218, 19, 46, 150, 39, 165, 141, 118, 163, 176, 184, 235, 113, 50, 155, 217, 114, 175, 202, 239, 252, 191, 56, 12, 231, 120, 203, 17, 65, 115, 131, 32, 20, 12, 118, 178, 253, 232, 165, 58, 147, 255, 105, 223, 144, 125, 98, 183, 12, 199, 33, 37, 254, 127, 212, 244, 67, 46, 166, 97, 248, 147, 60, 41, 153, 219, 15, 212, 132, 168, 49, 214, 15, 109, 16, 2, 254, 125, 63, 146, 131, 102, 212, 72, 61, 136, 6, 243, 26, 109, 33, 197, 31, 85, 190, 147, 86, 60, 78, 96, 131 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("6269416f-27ee-4176-8011-20dd82129101"),
                            Email = "teacher@mail.ru",
                            PasswordHash = new byte[] { 128, 178, 13, 4, 227, 43, 62, 132, 14, 8, 225, 37, 124, 151, 141, 111, 245, 222, 255, 49, 110, 50, 133, 190, 9, 150, 69, 229, 24, 84, 208, 214, 127, 21, 35, 174, 44, 50, 196, 141, 68, 139, 112, 183, 159, 101, 232, 54, 53, 136, 186, 198, 27, 122, 96, 195, 87, 48, 175, 189, 77, 43, 189, 113 },
                            PasswordSalt = new byte[] { 58, 2, 81, 141, 227, 88, 60, 238, 134, 2, 53, 168, 178, 138, 15, 184, 7, 170, 1, 244, 193, 106, 238, 186, 123, 210, 59, 142, 64, 121, 207, 188, 103, 33, 168, 20, 164, 9, 32, 38, 190, 25, 193, 1, 73, 185, 39, 218, 84, 140, 241, 127, 225, 26, 208, 91, 243, 54, 122, 240, 37, 217, 182, 47, 53, 221, 200, 165, 37, 206, 169, 154, 100, 73, 116, 82, 62, 240, 227, 245, 90, 77, 225, 66, 15, 208, 225, 186, 20, 31, 171, 49, 133, 101, 251, 44, 228, 175, 251, 119, 226, 162, 61, 119, 40, 218, 27, 122, 207, 89, 92, 104, 247, 122, 162, 193, 88, 207, 89, 40, 155, 13, 181, 80, 204, 163, 56, 192 },
                            TokenCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TokenExpires = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = new Guid("76a229cb-fcad-4453-9325-409be0c798fb"),
                            Email = "teacherAdmin@mail.ru",
                            PasswordHash = new byte[] { 18, 99, 223, 62, 165, 6, 242, 222, 255, 74, 151, 80, 99, 220, 61, 152, 234, 162, 2, 173, 104, 74, 192, 244, 218, 123, 162, 47, 70, 227, 206, 193, 156, 8, 161, 40, 244, 227, 69, 41, 59, 201, 163, 88, 35, 104, 206, 50, 93, 248, 12, 177, 235, 32, 120, 246, 164, 159, 167, 132, 169, 235, 185, 232 },
                            PasswordSalt = new byte[] { 2, 42, 175, 153, 104, 125, 191, 105, 87, 116, 65, 115, 143, 249, 214, 113, 63, 102, 209, 135, 162, 170, 32, 120, 74, 65, 249, 219, 121, 211, 61, 147, 27, 59, 6, 231, 188, 185, 63, 31, 148, 152, 150, 116, 1, 5, 41, 215, 155, 83, 26, 111, 74, 186, 131, 49, 142, 148, 222, 241, 239, 214, 170, 77, 75, 28, 157, 238, 213, 78, 208, 91, 240, 253, 177, 41, 201, 234, 241, 169, 61, 234, 40, 25, 56, 102, 89, 160, 90, 14, 60, 237, 8, 124, 229, 136, 245, 250, 148, 168, 22, 107, 173, 124, 250, 172, 115, 185, 247, 1, 238, 157, 58, 75, 191, 202, 59, 181, 181, 196, 50, 5, 247, 71, 135, 124, 197, 117 },
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
                            Id = new Guid("3e0a4341-24f8-46d5-bda7-ca06c2c96fc9"),
                            SubjectNameApiTable = "История России",
                            SubjectNameCurriculum = "История России"
                        },
                        new
                        {
                            Id = new Guid("d9c5ca00-eb04-44b8-a9a8-8625c28daaea"),
                            SubjectNameApiTable = "Управление программными проектами",
                            SubjectNameCurriculum = "Управление программными проектами"
                        },
                        new
                        {
                            Id = new Guid("fc5d6cec-1e55-4b6a-b018-26c423e94e25"),
                            SubjectNameApiTable = "Самостоятельная работа студента"
                        },
                        new
                        {
                            Id = new Guid("0a4f786c-0025-4849-a12a-7b346c257159"),
                            SubjectNameApiTable = "Экономика программной инженерии",
                            SubjectNameCurriculum = "Экономика программной инженерии"
                        },
                        new
                        {
                            Id = new Guid("0f24d7e6-186a-4330-bcb6-879e0b2ea832"),
                            SubjectNameApiTable = "Микропроцессорные системы",
                            SubjectNameCurriculum = "Микропроцессорные системы"
                        },
                        new
                        {
                            Id = new Guid("e2fe21eb-ede1-41eb-ac2e-3ff2475431c0"),
                            SubjectNameApiTable = "Сопровождение программного обеспечения",
                            SubjectNameCurriculum = "Сопровождение программного обеспечения"
                        },
                        new
                        {
                            Id = new Guid("dc8b45c0-d543-4ebb-bd69-bf57d0c8df1c"),
                            SubjectNameApiTable = "Математический аhализ",
                            SubjectNameCurriculum = "Математический анализ"
                        },
                        new
                        {
                            Id = new Guid("65cf7f00-0d53-4708-95a8-c008cacc39f2"),
                            SubjectNameApiTable = "Проектирование человеко-машинного интерфейса",
                            SubjectNameCurriculum = "Проектирование человеко-машинного интерфейса"
                        },
                        new
                        {
                            Id = new Guid("cacc1b49-f67e-4726-8665-c207da5f3b37"),
                            SubjectNameApiTable = "Разработка и анализ требований , конструирование программного обеспечения",
                            SubjectNameCurriculum = "Разработка и анализ требований, конструирование программного обеспечения"
                        },
                        new
                        {
                            Id = new Guid("5ce732d5-0990-4a45-a9ed-f8736fbf908b"),
                            SubjectNameApiTable = "Теория принятия решений",
                            SubjectNameCurriculum = "Теория принятия решений"
                        },
                        new
                        {
                            Id = new Guid("6c2d3e58-6163-44f7-9b2a-376e0b8ff4b9"),
                            SubjectNameApiTable = "Разработка приложений asp.net",
                            SubjectNameCurriculum = "Разработка приложений ASP.NET"
                        },
                        new
                        {
                            Id = new Guid("bf4134c8-be99-4388-8418-14bbacdbcc1d"),
                            SubjectNameApiTable = "Субд postgresql",
                            SubjectNameCurriculum = "СУБД PostgreSQL"
                        },
                        new
                        {
                            Id = new Guid("aeb62342-67a3-47e9-8537-745752beab53"),
                            SubjectNameApiTable = "Элективные дисциплины по физической культуре и спорту",
                            SubjectNameCurriculum = "Элективные дисциплины по физической культуре и спорту"
                        },
                        new
                        {
                            Id = new Guid("ae2d1705-4a2c-406a-a011-a4965f94ca75"),
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
                        .HasFilter("\"RoleId\" = '22c95dbf-735a-434b-b1be-437445b4dd2e'");

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
                            RoleId = new Guid("cae29eb6-e185-4a06-a83d-0ed776daab40"),
                            UserId = new Guid("9c9d1d90-5a6a-4882-816d-f3b8a3fc528c")
                        },
                        new
                        {
                            RoleId = new Guid("22c95dbf-735a-434b-b1be-437445b4dd2e"),
                            UserId = new Guid("2a68d924-7f89-4e89-9488-66b40dcb2226")
                        },
                        new
                        {
                            RoleId = new Guid("7102ba69-f7b6-42e4-abc1-fe48aa08cf12"),
                            UserId = new Guid("6269416f-27ee-4176-8011-20dd82129101")
                        },
                        new
                        {
                            RoleId = new Guid("7102ba69-f7b6-42e4-abc1-fe48aa08cf12"),
                            UserId = new Guid("76a229cb-fcad-4453-9325-409be0c798fb")
                        },
                        new
                        {
                            RoleId = new Guid("cae29eb6-e185-4a06-a83d-0ed776daab40"),
                            UserId = new Guid("76a229cb-fcad-4453-9325-409be0c798fb")
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

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.LectureLesson", b =>
                {
                    b.HasBaseType("AcademicProgressTracker.Domain.Entities.Lesson");

                    b.HasDiscriminator().HasValue("LectureLesson");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.PracticeLesson", b =>
                {
                    b.HasBaseType("AcademicProgressTracker.Domain.Entities.Lesson");

                    b.HasDiscriminator().HasValue("PracticeLesson");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.StudentProfile", b =>
                {
                    b.HasBaseType("AcademicProgressTracker.Domain.Entities.Profile");

                    b.HasDiscriminator().HasValue("StudentProfile");
                });

            modelBuilder.Entity("AcademicProgressTracker.Domain.Entities.TeacherProfile", b =>
                {
                    b.HasBaseType("AcademicProgressTracker.Domain.Entities.Profile");

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
