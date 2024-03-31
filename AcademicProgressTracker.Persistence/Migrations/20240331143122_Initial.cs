using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademicProgressTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CurriculumExcelDocument = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LabWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectMappings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectNameApiTable = table.Column<string>(type: "text", nullable: false),
                    SubjectNameCurriculum = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectMappings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TokenExpires = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LabWorkUserStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsDone = table.Column<bool>(type: "boolean", nullable: false),
                    LabWorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabWorkUserStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabWorkUserStatuses_LabWorks_LabWorkId",
                        column: x => x.LabWorkId,
                        principalTable: "LabWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabWorkUserStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    TeacherProfile_Name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RealStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RealEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsStarted = table.Column<bool>(type: "boolean", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    LabWorkId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_LabWorks_LabWorkId",
                        column: x => x.LabWorkId,
                        principalTable: "LabWorks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubject",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubject", x => new { x.SubjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherSubject_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonUserStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsVisited = table.Column<bool>(type: "boolean", nullable: false),
                    Score = table.Column<decimal>(type: "numeric", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonUserStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonUserStatuses_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonUserStatuses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0b595b0f-948d-4f48-b664-e5c944eb0764"), "Admin" },
                    { new Guid("c8720f64-b4de-41a5-a621-66a78645bdbb"), "Student" },
                    { new Guid("d6b3cac4-eb6e-49e7-a61f-68e2a539c148"), "Teacher" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("01e2c4ab-122e-4833-b5b3-d2c049bc89c2"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("1975ff47-1e8d-4d84-ac03-3fc3ba261ca6"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("220b08d3-1b34-4537-a5a1-eb350d57339a"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("2f5e9de1-5d63-4d6b-ae34-bef2da940220"), "Самостоятельная работа студента", null },
                    { new Guid("730f97d4-6615-4b77-a824-024747014acc"), "История России", "История России" },
                    { new Guid("78932038-f1cd-4f2f-bd89-c04242083be6"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("79097678-fc47-4a6b-bf04-10978c6581ae"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("a0176ad7-b1cc-42de-bacf-518da2780b7f"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("d0c381bc-e011-4d95-b55d-35e5d4f4a9b7"), "Математический аhализ", "Математический анализ" },
                    { new Guid("d1ef0e71-2302-4b22-b9ad-dc6de22f94db"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("d6a31898-e4db-48cd-931d-e067d1cbac7e"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("ee867d47-389a-4e40-a1ee-70d92895c434"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("f36534a7-4ef2-47c4-bf66-5ea6b08176bc"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("f7bb641d-7553-4bae-bd7a-5fde0b3bc76b"), "Экономика программной инженерии", "Экономика программной инженерии" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("2cc61ea2-26b8-44c6-98fd-ceaa04b60b69"), "teacherAdmin@mail.ru", new byte[] { 155, 245, 40, 206, 168, 20, 236, 118, 36, 27, 255, 224, 152, 49, 103, 27, 169, 47, 235, 123, 226, 100, 25, 113, 53, 188, 28, 62, 184, 35, 196, 127, 180, 195, 61, 173, 221, 255, 227, 46, 188, 105, 2, 210, 104, 119, 79, 251, 252, 166, 42, 65, 83, 81, 34, 171, 68, 135, 109, 59, 193, 57, 27, 136 }, new byte[] { 224, 147, 237, 149, 75, 233, 31, 65, 119, 151, 79, 107, 7, 30, 3, 185, 132, 245, 137, 163, 152, 225, 52, 158, 13, 75, 31, 48, 173, 136, 195, 23, 25, 16, 226, 27, 90, 102, 32, 241, 119, 203, 57, 148, 0, 112, 82, 231, 227, 76, 114, 66, 126, 53, 81, 194, 22, 132, 195, 64, 173, 142, 210, 5, 159, 48, 220, 166, 112, 228, 167, 95, 131, 233, 84, 166, 95, 75, 148, 36, 183, 189, 183, 8, 90, 68, 242, 101, 178, 235, 33, 109, 117, 46, 96, 60, 147, 62, 192, 202, 108, 211, 175, 12, 30, 230, 184, 215, 105, 228, 220, 156, 124, 149, 189, 82, 85, 114, 205, 196, 61, 99, 59, 9, 108, 174, 217, 62 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("708dc5ee-0abe-4f3b-842a-6c2f1930e0d9"), "teacher@mail.ru", new byte[] { 99, 85, 182, 221, 223, 3, 218, 244, 18, 157, 31, 68, 194, 66, 154, 218, 235, 223, 139, 80, 38, 119, 185, 153, 235, 225, 106, 0, 243, 204, 89, 200, 180, 138, 143, 148, 32, 217, 67, 254, 144, 46, 243, 36, 13, 183, 62, 84, 112, 125, 90, 55, 132, 73, 164, 109, 206, 187, 93, 0, 152, 56, 202, 162 }, new byte[] { 220, 58, 109, 77, 194, 22, 14, 206, 161, 170, 128, 75, 206, 191, 35, 212, 35, 17, 128, 143, 185, 111, 216, 66, 205, 143, 3, 120, 203, 198, 90, 30, 24, 120, 30, 18, 40, 194, 32, 168, 190, 106, 11, 114, 68, 236, 238, 65, 193, 79, 217, 99, 161, 218, 116, 114, 101, 202, 76, 107, 10, 85, 184, 85, 50, 154, 116, 138, 118, 167, 93, 178, 36, 228, 9, 208, 32, 254, 38, 115, 59, 198, 155, 247, 221, 32, 198, 198, 92, 51, 61, 216, 183, 135, 85, 205, 46, 24, 38, 125, 13, 143, 101, 197, 190, 109, 33, 110, 125, 102, 237, 74, 93, 163, 145, 112, 155, 154, 134, 42, 65, 188, 3, 250, 201, 147, 233, 8 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9699fccd-e0f6-41d4-a8a2-3b03e5591192"), "admin@mail.ru", new byte[] { 94, 22, 230, 189, 161, 65, 77, 233, 215, 89, 101, 217, 31, 160, 41, 216, 33, 186, 104, 64, 146, 3, 118, 168, 102, 16, 147, 122, 132, 136, 145, 126, 72, 65, 96, 144, 193, 240, 165, 28, 195, 184, 221, 131, 10, 126, 62, 206, 110, 215, 61, 149, 100, 64, 251, 140, 99, 2, 42, 48, 21, 180, 106, 126 }, new byte[] { 205, 54, 157, 11, 172, 71, 250, 177, 99, 185, 27, 169, 78, 239, 227, 139, 19, 215, 164, 255, 254, 88, 206, 78, 45, 228, 21, 253, 211, 217, 95, 90, 5, 167, 105, 57, 77, 241, 56, 61, 190, 97, 209, 83, 21, 140, 83, 216, 86, 200, 189, 32, 1, 188, 108, 67, 123, 183, 252, 215, 203, 182, 4, 111, 2, 89, 85, 154, 84, 61, 217, 150, 87, 137, 4, 20, 18, 67, 37, 9, 177, 192, 96, 77, 180, 230, 186, 108, 105, 113, 61, 55, 154, 20, 129, 230, 199, 146, 113, 175, 246, 66, 45, 208, 154, 250, 26, 52, 131, 46, 75, 184, 80, 49, 214, 42, 2, 140, 124, 153, 39, 248, 4, 83, 9, 165, 133, 240 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("dbe9aa58-c3f9-4ee2-99b3-53a725685db6"), "student@mail.ru", new byte[] { 127, 33, 176, 182, 26, 222, 109, 245, 204, 118, 115, 215, 188, 231, 182, 232, 191, 176, 134, 122, 176, 200, 197, 191, 99, 141, 207, 196, 70, 117, 127, 74, 61, 109, 79, 112, 112, 30, 4, 75, 100, 107, 82, 74, 35, 202, 172, 221, 31, 148, 231, 107, 91, 130, 129, 232, 182, 3, 194, 9, 113, 36, 196, 82 }, new byte[] { 248, 92, 89, 70, 88, 120, 247, 63, 15, 197, 249, 240, 95, 244, 0, 180, 186, 247, 59, 124, 61, 248, 20, 221, 199, 150, 143, 159, 5, 98, 131, 210, 106, 169, 243, 82, 33, 44, 41, 13, 95, 73, 170, 159, 249, 222, 135, 30, 239, 49, 39, 50, 229, 212, 176, 254, 92, 237, 28, 16, 125, 181, 170, 138, 155, 199, 80, 170, 31, 8, 66, 82, 249, 16, 60, 70, 111, 105, 66, 11, 39, 250, 226, 105, 171, 186, 103, 96, 105, 51, 233, 201, 254, 200, 177, 13, 192, 189, 139, 9, 112, 108, 255, 153, 191, 46, 203, 192, 199, 162, 185, 201, 3, 159, 255, 111, 42, 47, 105, 25, 251, 251, 126, 198, 24, 30, 92, 102 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0b595b0f-948d-4f48-b664-e5c944eb0764"), new Guid("2cc61ea2-26b8-44c6-98fd-ceaa04b60b69") },
                    { new Guid("0b595b0f-948d-4f48-b664-e5c944eb0764"), new Guid("9699fccd-e0f6-41d4-a8a2-3b03e5591192") },
                    { new Guid("c8720f64-b4de-41a5-a621-66a78645bdbb"), new Guid("dbe9aa58-c3f9-4ee2-99b3-53a725685db6") },
                    { new Guid("d6b3cac4-eb6e-49e7-a61f-68e2a539c148"), new Guid("2cc61ea2-26b8-44c6-98fd-ceaa04b60b69") },
                    { new Guid("d6b3cac4-eb6e-49e7-a61f-68e2a539c148"), new Guid("708dc5ee-0abe-4f3b-842a-6c2f1930e0d9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabWorkUserStatuses_LabWorkId",
                table: "LabWorkUserStatuses",
                column: "LabWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_LabWorkUserStatuses_UserId",
                table: "LabWorkUserStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUserStatuses_LessonId",
                table: "LessonUserStatuses",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonUserStatuses_UserId",
                table: "LessonUserStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_LabWorkId",
                table: "Lessons",
                column: "LabWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GroupId",
                table: "Subjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubject_UserId",
                table: "TeacherSubject",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_RoleId",
                table: "UserGroup",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"RoleId\" = 'c8720f64-b4de-41a5-a621-66a78645bdbb'");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabWorkUserStatuses");

            migrationBuilder.DropTable(
                name: "LessonUserStatuses");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "SubjectMappings");

            migrationBuilder.DropTable(
                name: "TeacherSubject");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LabWorks");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
