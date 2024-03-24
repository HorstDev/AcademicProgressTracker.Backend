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
                    { new Guid("0b05b3c0-f694-4752-becb-4e8155138857"), "Admin" },
                    { new Guid("a4a6fa59-75c0-4dc0-8c85-3ff345916351"), "Teacher" },
                    { new Guid("a9376f5b-3bed-47db-8f45-6994f76983b6"), "Student" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("1a3b7afd-e5b6-49f1-bb4a-f6e605991662"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("28ec8a4f-93a8-4ec1-807e-4eecf90a93a3"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("4edda1f3-aa9d-4bd1-83b1-9e363d5dc8dc"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("6c713f9f-2e68-4038-a3e8-4730e4bfbd1d"), "Математический аhализ", "Математический анализ" },
                    { new Guid("8999db37-6355-4d83-bf17-1c9e1a7987b0"), "Самостоятельная работа студента", null },
                    { new Guid("8ae774e4-d061-4017-8faf-0b7e6729acc1"), "История россии", "История России" },
                    { new Guid("9a710d16-e7d2-44ea-84ae-972f9ca57065"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("abf2ca3d-f4ea-44b7-8ab2-6a3e2014eb9d"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("af80cca5-ecb8-49c0-b852-4c30a9ff8d0f"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("b0392c38-25af-4adf-9ed4-b3c5ea17a4a6"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("b8f63775-7bc5-474c-ae01-f666cc1a45c4"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("c0df2ab0-7d31-4538-9426-a5e411e84628"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("d462704e-0219-49fa-be18-ab5ad00af383"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("ed807016-d6fa-4ea9-9cb4-77ed5db33617"), "Теория принятия решений", "Теория принятия решений" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("482c2c66-c3ac-4673-addd-1e29a8a06528"), "student@mail.ru", new byte[] { 180, 176, 208, 171, 183, 59, 84, 219, 154, 152, 94, 47, 91, 82, 170, 40, 18, 198, 160, 41, 102, 36, 163, 195, 6, 71, 245, 107, 82, 153, 21, 160, 59, 19, 165, 99, 253, 232, 248, 157, 112, 57, 125, 180, 143, 17, 167, 135, 200, 225, 24, 170, 186, 252, 103, 222, 2, 255, 110, 180, 198, 34, 202, 247 }, new byte[] { 154, 16, 107, 179, 132, 189, 180, 15, 68, 188, 228, 59, 160, 41, 228, 255, 206, 207, 23, 196, 38, 109, 84, 26, 2, 181, 48, 76, 64, 191, 149, 57, 81, 177, 142, 248, 99, 21, 40, 89, 176, 213, 126, 205, 224, 243, 178, 100, 169, 147, 1, 170, 46, 104, 158, 68, 7, 223, 46, 241, 56, 62, 168, 253, 199, 138, 208, 32, 146, 217, 64, 213, 97, 48, 147, 206, 105, 58, 250, 64, 247, 7, 246, 100, 122, 230, 216, 232, 16, 237, 11, 223, 129, 82, 147, 152, 248, 60, 10, 110, 173, 150, 31, 202, 191, 214, 229, 238, 97, 206, 155, 9, 1, 10, 57, 189, 236, 162, 105, 44, 15, 37, 211, 129, 94, 136, 8, 213 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4caf24c0-3853-4ef0-9153-c4ce82da5f76"), "admin@mail.ru", new byte[] { 145, 107, 169, 145, 78, 42, 39, 233, 28, 229, 142, 88, 137, 247, 127, 11, 253, 242, 168, 221, 237, 217, 98, 45, 159, 157, 208, 177, 164, 38, 89, 211, 149, 143, 252, 102, 30, 47, 214, 143, 159, 250, 174, 36, 133, 225, 189, 34, 188, 12, 248, 163, 103, 18, 139, 223, 173, 213, 104, 217, 206, 91, 70, 232 }, new byte[] { 230, 80, 209, 85, 65, 248, 185, 198, 167, 50, 102, 13, 58, 2, 139, 169, 96, 131, 58, 96, 185, 225, 241, 71, 17, 10, 56, 79, 233, 87, 28, 114, 96, 174, 3, 173, 254, 229, 69, 74, 30, 111, 49, 57, 99, 235, 60, 23, 34, 104, 45, 172, 20, 136, 9, 169, 73, 93, 149, 251, 13, 30, 105, 124, 224, 24, 171, 149, 74, 66, 94, 31, 37, 49, 149, 195, 205, 176, 66, 153, 123, 158, 107, 193, 92, 93, 245, 80, 91, 144, 116, 83, 153, 200, 73, 62, 203, 131, 106, 24, 61, 103, 247, 135, 190, 32, 164, 158, 178, 91, 34, 25, 172, 255, 198, 72, 243, 19, 68, 29, 44, 26, 56, 109, 42, 245, 220, 124 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("74f0850c-b054-4d88-a29e-ebe765e67810"), "teacher@mail.ru", new byte[] { 248, 136, 239, 246, 83, 49, 248, 96, 188, 16, 236, 222, 75, 164, 100, 229, 161, 94, 27, 35, 160, 150, 113, 110, 16, 66, 43, 56, 66, 241, 192, 110, 212, 118, 20, 160, 179, 143, 167, 168, 187, 198, 253, 201, 75, 182, 40, 98, 72, 48, 162, 27, 14, 68, 40, 251, 11, 105, 60, 92, 228, 143, 103, 223 }, new byte[] { 101, 63, 80, 219, 220, 106, 151, 196, 195, 49, 223, 96, 132, 45, 50, 167, 78, 67, 77, 203, 124, 32, 56, 165, 64, 230, 172, 142, 66, 29, 127, 123, 249, 44, 229, 221, 48, 236, 61, 120, 156, 88, 195, 210, 232, 178, 2, 243, 16, 92, 85, 111, 108, 155, 66, 199, 176, 36, 189, 130, 238, 211, 189, 64, 10, 205, 52, 94, 14, 53, 157, 53, 88, 201, 0, 158, 236, 100, 160, 252, 189, 205, 115, 61, 134, 86, 57, 166, 89, 252, 101, 199, 20, 20, 72, 161, 240, 34, 191, 164, 198, 28, 159, 170, 2, 14, 155, 82, 152, 16, 99, 149, 199, 28, 245, 154, 110, 193, 88, 17, 253, 207, 8, 100, 90, 69, 226, 254 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fa47d919-e6b6-47b8-b6d8-5834c2303b93"), "teacherAdmin@mail.ru", new byte[] { 248, 104, 242, 63, 130, 146, 247, 162, 56, 23, 151, 106, 239, 38, 16, 56, 47, 212, 243, 62, 222, 94, 44, 43, 9, 24, 87, 177, 143, 21, 108, 181, 235, 26, 236, 14, 226, 145, 105, 41, 217, 196, 102, 198, 118, 200, 113, 130, 133, 1, 191, 16, 168, 39, 87, 131, 135, 151, 80, 202, 69, 169, 149, 105 }, new byte[] { 67, 207, 77, 53, 242, 250, 172, 10, 29, 125, 89, 70, 157, 238, 220, 21, 46, 62, 26, 64, 219, 79, 82, 245, 210, 93, 49, 166, 6, 182, 181, 125, 145, 85, 218, 23, 64, 161, 12, 44, 196, 113, 14, 141, 46, 97, 148, 94, 8, 100, 76, 203, 173, 177, 80, 134, 114, 72, 9, 36, 247, 84, 211, 2, 5, 237, 103, 35, 39, 234, 207, 173, 53, 150, 83, 120, 164, 212, 221, 161, 233, 220, 159, 174, 14, 37, 114, 130, 106, 253, 52, 170, 71, 200, 38, 233, 133, 191, 40, 79, 12, 21, 128, 4, 40, 168, 132, 10, 155, 53, 243, 252, 50, 38, 54, 151, 246, 231, 62, 53, 216, 230, 60, 156, 130, 123, 27, 100 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0b05b3c0-f694-4752-becb-4e8155138857"), new Guid("4caf24c0-3853-4ef0-9153-c4ce82da5f76") },
                    { new Guid("0b05b3c0-f694-4752-becb-4e8155138857"), new Guid("fa47d919-e6b6-47b8-b6d8-5834c2303b93") },
                    { new Guid("a4a6fa59-75c0-4dc0-8c85-3ff345916351"), new Guid("74f0850c-b054-4d88-a29e-ebe765e67810") },
                    { new Guid("a4a6fa59-75c0-4dc0-8c85-3ff345916351"), new Guid("fa47d919-e6b6-47b8-b6d8-5834c2303b93") },
                    { new Guid("a9376f5b-3bed-47db-8f45-6994f76983b6"), new Guid("482c2c66-c3ac-4673-addd-1e29a8a06528") }
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
                filter: "\"RoleId\" = 'a9376f5b-3bed-47db-8f45-6994f76983b6'");

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
