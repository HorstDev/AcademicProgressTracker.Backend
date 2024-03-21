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
                    Start = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    End = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
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
                    { new Guid("007d022d-bb70-41b5-8dc7-a3ef604b32ff"), "Student" },
                    { new Guid("2704061f-3dcb-49e2-9c50-f4f2d2c4bb5c"), "Teacher" },
                    { new Guid("3d263beb-0f41-4cc0-83a4-fc7c9390544c"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("12109d3a-bafa-46e4-bbe7-b068507b71c4"), "Математический аhализ", "Математический анализ" },
                    { new Guid("2a44eb2b-feef-4dd0-9631-5f8c84555103"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("351684ae-2d6e-4e8d-9f08-6e020aea6dd8"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("6b235256-375f-4369-8658-aec13535dd08"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("71b84b42-b989-4b1b-9c4c-e53b3665d87e"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("7f94e467-fe73-4206-965b-a5bbb910ff36"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("8dff8d62-6b75-4624-846a-52e92b63410d"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("9473ecfa-82d0-49b9-bec0-54ad4ece2704"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("c12b8075-f23e-4a6b-9a41-3bb234cc99e5"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("c33d25d9-e18c-4c36-9d56-b16bdfa0cdd8"), "История россии", "История России" },
                    { new Guid("ef63ee51-eef4-4cef-8816-3b7806a1fcec"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("f17634b7-d6c0-4b73-944f-38edafd3706b"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("fb699d5f-5e27-4a8e-aa5a-5551ee6d5df7"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("fed3fa08-d172-4814-a2c0-d1d14beff176"), "Самостоятельная работа студента", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("1b9e72e4-d00f-4fe3-acec-36e970b1bb2b"), "admin@mail.ru", new byte[] { 166, 252, 20, 131, 247, 251, 165, 205, 25, 83, 207, 39, 117, 223, 249, 27, 135, 133, 220, 128, 23, 58, 189, 78, 136, 210, 157, 108, 97, 100, 30, 161, 60, 97, 115, 90, 81, 96, 64, 216, 124, 190, 112, 208, 25, 21, 81, 71, 246, 12, 157, 198, 144, 41, 63, 17, 174, 130, 87, 232, 2, 34, 207, 100 }, new byte[] { 39, 118, 12, 157, 212, 204, 124, 127, 132, 93, 153, 136, 232, 63, 202, 123, 35, 19, 124, 223, 228, 145, 134, 151, 194, 109, 73, 61, 176, 187, 32, 28, 169, 92, 111, 27, 142, 48, 202, 143, 175, 17, 238, 15, 135, 249, 171, 161, 65, 170, 228, 49, 9, 156, 218, 244, 181, 185, 79, 227, 89, 51, 192, 11, 53, 16, 129, 48, 3, 43, 164, 160, 46, 210, 62, 231, 171, 27, 55, 32, 29, 224, 128, 30, 152, 74, 111, 134, 140, 2, 41, 52, 113, 76, 239, 149, 103, 206, 83, 69, 61, 76, 97, 147, 247, 77, 164, 29, 58, 90, 223, 197, 32, 9, 19, 203, 103, 104, 250, 23, 68, 45, 98, 1, 221, 251, 16, 184 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7b1a64b3-b8c7-4c3b-a955-1444ae27f041"), "teacher@mail.ru", new byte[] { 105, 157, 59, 95, 4, 91, 80, 252, 156, 42, 219, 213, 101, 252, 114, 227, 238, 17, 171, 143, 251, 163, 1, 89, 112, 231, 209, 169, 28, 57, 237, 102, 69, 185, 196, 182, 146, 10, 122, 134, 231, 201, 52, 74, 116, 196, 16, 216, 216, 153, 34, 16, 96, 22, 161, 229, 139, 171, 154, 52, 11, 178, 238, 198 }, new byte[] { 247, 180, 28, 160, 44, 214, 87, 246, 31, 191, 253, 201, 59, 169, 220, 94, 183, 45, 83, 191, 14, 180, 22, 13, 23, 48, 230, 228, 168, 79, 233, 246, 134, 96, 185, 224, 49, 57, 30, 170, 128, 166, 143, 167, 148, 95, 39, 2, 84, 83, 60, 249, 103, 96, 238, 146, 160, 145, 141, 47, 170, 23, 154, 35, 233, 101, 17, 154, 235, 249, 11, 29, 210, 30, 198, 249, 66, 35, 102, 79, 122, 78, 85, 19, 130, 79, 13, 27, 41, 172, 72, 221, 29, 42, 199, 48, 229, 52, 144, 66, 185, 43, 216, 171, 86, 253, 207, 21, 26, 95, 142, 132, 77, 105, 249, 161, 25, 99, 48, 101, 129, 198, 122, 231, 148, 181, 85, 47 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("b93008fb-c219-4165-8ec8-fd9a02bbd700"), "student@mail.ru", new byte[] { 110, 156, 93, 47, 0, 70, 149, 65, 96, 248, 4, 190, 137, 160, 196, 207, 180, 72, 154, 56, 185, 112, 40, 191, 35, 70, 254, 160, 220, 30, 10, 4, 195, 43, 177, 74, 52, 87, 225, 124, 229, 166, 79, 34, 176, 155, 208, 205, 173, 203, 78, 109, 247, 231, 15, 63, 156, 69, 178, 79, 216, 184, 7, 60 }, new byte[] { 171, 69, 186, 248, 139, 18, 175, 128, 222, 167, 14, 107, 3, 7, 125, 34, 181, 113, 169, 222, 107, 38, 198, 172, 82, 250, 4, 209, 61, 6, 158, 94, 102, 33, 111, 207, 117, 180, 18, 138, 57, 154, 150, 15, 153, 76, 182, 213, 169, 206, 255, 132, 148, 189, 66, 124, 199, 113, 188, 136, 83, 173, 204, 95, 61, 230, 123, 100, 188, 196, 41, 177, 125, 164, 25, 137, 179, 58, 137, 51, 115, 146, 40, 73, 223, 112, 53, 55, 64, 202, 103, 2, 210, 8, 62, 117, 56, 190, 61, 228, 40, 180, 203, 156, 52, 117, 48, 69, 45, 222, 113, 237, 34, 248, 13, 13, 112, 113, 178, 201, 249, 127, 127, 54, 244, 25, 9, 85 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("bc7a347d-b2c1-4051-9c91-c5d743d58b1e"), "teacherAdmin@mail.ru", new byte[] { 18, 8, 241, 79, 59, 126, 149, 1, 219, 69, 4, 197, 15, 166, 238, 60, 250, 231, 225, 196, 37, 12, 145, 182, 36, 105, 134, 121, 137, 114, 119, 140, 152, 190, 50, 97, 143, 109, 179, 172, 240, 185, 18, 250, 123, 244, 103, 187, 88, 28, 174, 224, 77, 49, 36, 184, 4, 124, 186, 6, 91, 204, 110, 162 }, new byte[] { 237, 198, 78, 155, 102, 46, 51, 31, 103, 52, 185, 50, 12, 22, 74, 164, 207, 202, 25, 15, 100, 115, 242, 3, 172, 1, 86, 248, 11, 42, 238, 170, 124, 19, 118, 184, 139, 92, 238, 231, 45, 28, 67, 238, 102, 99, 45, 174, 0, 1, 73, 174, 164, 192, 167, 209, 235, 67, 177, 232, 3, 254, 208, 55, 99, 250, 59, 38, 70, 219, 80, 76, 243, 37, 119, 166, 130, 221, 227, 54, 82, 242, 47, 254, 246, 52, 166, 143, 103, 70, 5, 141, 169, 206, 87, 105, 70, 120, 149, 69, 12, 214, 10, 5, 234, 45, 51, 185, 66, 235, 30, 128, 233, 59, 182, 101, 147, 8, 167, 169, 215, 254, 187, 184, 34, 219, 112, 176 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("007d022d-bb70-41b5-8dc7-a3ef604b32ff"), new Guid("b93008fb-c219-4165-8ec8-fd9a02bbd700") },
                    { new Guid("2704061f-3dcb-49e2-9c50-f4f2d2c4bb5c"), new Guid("7b1a64b3-b8c7-4c3b-a955-1444ae27f041") },
                    { new Guid("2704061f-3dcb-49e2-9c50-f4f2d2c4bb5c"), new Guid("bc7a347d-b2c1-4051-9c91-c5d743d58b1e") },
                    { new Guid("3d263beb-0f41-4cc0-83a4-fc7c9390544c"), new Guid("1b9e72e4-d00f-4fe3-acec-36e970b1bb2b") },
                    { new Guid("3d263beb-0f41-4cc0-83a4-fc7c9390544c"), new Guid("bc7a347d-b2c1-4051-9c91-c5d743d58b1e") }
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
                filter: "\"RoleId\" = '007d022d-bb70-41b5-8dc7-a3ef604b32ff'");

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
