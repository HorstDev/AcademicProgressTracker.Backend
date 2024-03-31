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
                    { new Guid("95759739-3e09-4cbc-b09c-35bdb61e4176"), "Teacher" },
                    { new Guid("da687bc4-c3ea-4a90-bfb2-aa5074f80b3e"), "Student" },
                    { new Guid("fc80a892-0a04-42d0-bb53-170e294e494c"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("2fcf9e37-6db1-4d02-a719-14f6eb9ec93c"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("301a6894-ac07-46d0-90f5-3fb8d08420b6"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("3653b918-1bdb-4e37-899b-634791005a48"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("41b0a3a7-6f9a-4633-bb6b-dfed51a69bfb"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("4cdb0bbf-2f9c-4b8a-90bf-4d51c0ff2bb0"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("571eb8fa-02a7-4065-93ae-6b27d933b53f"), "Самостоятельная работа студента", null },
                    { new Guid("62462bac-728f-4296-9d8d-156a9e7f8841"), "Математический аhализ", "Математический анализ" },
                    { new Guid("777f018e-c613-4250-b065-f3e14c88f906"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("ca0afc01-d3ef-496c-b6a3-266acfa7df27"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("d517279f-fa29-4dea-bcd6-1f9b5d81d161"), "История России", "История России" },
                    { new Guid("da63d4bd-7693-4ca4-aea3-79e800f4ffc2"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("ee85ddf9-a397-4d23-bb36-fbb2bf407307"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("f2cda0ca-65a5-455c-8d89-0732d2671b7f"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("f973d8de-a84b-4e9a-b83f-35c2fb67aae5"), "Управление программными проектами", "Управление программными проектами" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("1808c3ab-5740-4211-ad69-423e63ef4610"), "admin@mail.ru", new byte[] { 101, 2, 227, 54, 64, 155, 216, 88, 175, 128, 123, 224, 193, 177, 202, 199, 123, 64, 37, 114, 251, 209, 27, 237, 139, 153, 61, 75, 179, 152, 224, 134, 184, 86, 113, 54, 230, 190, 118, 165, 212, 136, 21, 5, 67, 199, 231, 169, 137, 131, 174, 70, 188, 189, 33, 98, 52, 171, 52, 221, 46, 37, 138, 121 }, new byte[] { 98, 109, 249, 58, 3, 175, 223, 100, 104, 155, 14, 170, 228, 164, 170, 5, 160, 70, 115, 150, 52, 223, 150, 76, 208, 89, 194, 85, 34, 1, 72, 230, 164, 57, 129, 141, 110, 170, 211, 219, 223, 229, 255, 79, 38, 158, 232, 193, 54, 73, 184, 47, 190, 206, 240, 95, 66, 167, 40, 126, 128, 72, 54, 202, 57, 162, 192, 103, 130, 17, 134, 155, 6, 64, 44, 148, 183, 164, 106, 80, 182, 72, 22, 130, 6, 202, 233, 138, 72, 96, 70, 30, 180, 223, 192, 132, 158, 24, 116, 224, 215, 233, 89, 29, 250, 208, 15, 178, 193, 61, 75, 144, 231, 86, 53, 142, 105, 116, 15, 30, 212, 193, 237, 121, 70, 9, 122, 243 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("196c4816-f101-46cc-9de9-1e363fd3565e"), "teacher@mail.ru", new byte[] { 43, 172, 15, 69, 49, 184, 80, 237, 32, 20, 49, 121, 101, 61, 227, 21, 69, 187, 85, 35, 187, 148, 239, 121, 6, 214, 236, 159, 148, 99, 61, 218, 134, 122, 253, 242, 119, 113, 18, 239, 251, 88, 41, 90, 180, 66, 161, 112, 235, 97, 71, 238, 66, 228, 198, 218, 212, 254, 143, 95, 235, 58, 176, 91 }, new byte[] { 254, 36, 183, 79, 109, 2, 145, 253, 142, 248, 53, 83, 32, 37, 241, 6, 65, 76, 36, 146, 30, 79, 250, 242, 113, 16, 182, 227, 181, 99, 126, 97, 96, 66, 44, 178, 223, 161, 14, 18, 63, 137, 51, 217, 202, 27, 32, 214, 243, 145, 81, 243, 174, 70, 255, 86, 208, 210, 131, 242, 37, 39, 143, 0, 51, 138, 17, 149, 236, 32, 26, 29, 236, 153, 183, 91, 22, 145, 126, 157, 60, 60, 47, 126, 133, 4, 108, 40, 239, 95, 28, 223, 109, 35, 215, 30, 72, 143, 206, 233, 186, 9, 231, 30, 208, 215, 232, 29, 3, 96, 31, 68, 151, 204, 8, 199, 177, 127, 172, 5, 27, 151, 116, 216, 129, 99, 238, 37 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1e409d26-b75f-45c7-924f-da8037557142"), "student@mail.ru", new byte[] { 67, 145, 96, 145, 36, 170, 139, 193, 1, 158, 206, 204, 121, 89, 177, 214, 2, 69, 248, 174, 110, 48, 86, 170, 58, 122, 30, 172, 148, 91, 103, 233, 168, 106, 165, 31, 231, 103, 78, 1, 171, 33, 12, 156, 143, 217, 165, 237, 117, 237, 13, 75, 46, 44, 171, 182, 246, 99, 64, 176, 73, 245, 37, 217 }, new byte[] { 74, 49, 19, 236, 199, 61, 6, 213, 142, 59, 207, 174, 228, 25, 175, 13, 248, 160, 136, 239, 129, 110, 211, 69, 2, 14, 180, 150, 40, 157, 19, 253, 145, 103, 147, 42, 182, 72, 126, 169, 232, 199, 34, 213, 89, 112, 126, 33, 183, 128, 251, 171, 52, 69, 72, 245, 44, 198, 77, 240, 119, 199, 248, 140, 206, 193, 106, 198, 180, 134, 233, 238, 140, 103, 185, 12, 117, 2, 39, 34, 125, 88, 184, 59, 10, 196, 221, 76, 190, 174, 26, 46, 180, 160, 229, 249, 140, 25, 8, 230, 193, 60, 57, 224, 71, 233, 155, 38, 58, 251, 24, 0, 250, 108, 131, 32, 154, 70, 250, 35, 127, 104, 148, 68, 63, 1, 112, 21 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d6dc0477-ae18-48ab-aee0-7adcd88b9f6d"), "teacherAdmin@mail.ru", new byte[] { 225, 173, 80, 207, 148, 9, 200, 154, 51, 91, 224, 125, 15, 215, 112, 97, 35, 245, 144, 34, 58, 122, 158, 133, 21, 120, 88, 72, 110, 137, 83, 31, 34, 33, 170, 81, 49, 180, 137, 138, 210, 23, 51, 153, 72, 150, 140, 33, 111, 45, 150, 198, 125, 234, 5, 93, 156, 197, 159, 24, 3, 197, 226, 235 }, new byte[] { 233, 97, 25, 105, 78, 243, 216, 179, 221, 55, 127, 147, 1, 130, 39, 250, 5, 143, 44, 167, 235, 14, 98, 122, 37, 52, 68, 108, 74, 85, 179, 180, 51, 210, 250, 8, 155, 98, 189, 9, 247, 98, 96, 48, 92, 43, 250, 6, 238, 67, 183, 55, 113, 218, 16, 199, 175, 64, 181, 156, 214, 170, 113, 5, 143, 50, 142, 142, 26, 26, 131, 207, 101, 239, 143, 59, 149, 143, 120, 103, 202, 191, 8, 107, 156, 106, 89, 209, 64, 160, 205, 156, 68, 173, 161, 112, 90, 84, 18, 58, 126, 161, 140, 14, 60, 218, 176, 114, 191, 130, 241, 76, 159, 129, 68, 251, 103, 253, 71, 71, 219, 220, 197, 164, 16, 103, 115, 61 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("95759739-3e09-4cbc-b09c-35bdb61e4176"), new Guid("196c4816-f101-46cc-9de9-1e363fd3565e") },
                    { new Guid("95759739-3e09-4cbc-b09c-35bdb61e4176"), new Guid("d6dc0477-ae18-48ab-aee0-7adcd88b9f6d") },
                    { new Guid("da687bc4-c3ea-4a90-bfb2-aa5074f80b3e"), new Guid("1e409d26-b75f-45c7-924f-da8037557142") },
                    { new Guid("fc80a892-0a04-42d0-bb53-170e294e494c"), new Guid("1808c3ab-5740-4211-ad69-423e63ef4610") },
                    { new Guid("fc80a892-0a04-42d0-bb53-170e294e494c"), new Guid("d6dc0477-ae18-48ab-aee0-7adcd88b9f6d") }
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
                filter: "\"RoleId\" = 'da687bc4-c3ea-4a90-bfb2-aa5074f80b3e'");

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
