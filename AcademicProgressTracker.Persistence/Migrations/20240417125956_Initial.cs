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
                    DateTimeOfUpdateDependenciesFromServer = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    Name = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false)
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
                    { new Guid("07aae6f7-7a94-4706-a8dc-fd55c912a96f"), "Student" },
                    { new Guid("5c06a1d3-40f0-4400-a6d7-41ebf90bf7ad"), "Teacher" },
                    { new Guid("6c308ee5-e256-4775-9c6a-22d804b8d038"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("138821a1-6064-43f9-a4eb-056fe501f67e"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("1b648bb0-851d-4def-87f8-130da1f5e814"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("31810980-3991-464b-8db6-109d0a062416"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("3c41a472-d456-4efe-ba82-1863b205fb7d"), "Самостоятельная работа студента", null },
                    { new Guid("4a104ad6-22fc-41dd-91ba-0ecaa0d6575b"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("4eb620f7-80f0-4ee6-9ac2-2622b078712b"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("5286df14-4007-4ac6-bd1d-d582fe68e046"), "История России", "История России" },
                    { new Guid("57a25ed1-0546-4722-a878-5d92c3ad2e38"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("608cb6d7-da55-4d06-a0ce-e5e05701a07d"), "Математический аhализ", "Математический анализ" },
                    { new Guid("64496ea7-338c-44df-8b90-0ef419e11c43"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("a1cea945-9352-403e-8651-ec4015336da6"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("a908c0a4-7550-4044-be0b-8f3931a6afc8"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("b9b7e2c0-9c87-4bfc-952e-d6e9baceb97d"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("e308958f-e55a-45f4-a7bd-bcd816ba4764"), "Теория принятия решений", "Теория принятия решений" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("462aa5e6-8626-46ba-96b5-26cab9984778"), "student@mail.ru", new byte[] { 88, 94, 134, 198, 200, 89, 136, 143, 80, 240, 219, 196, 134, 46, 138, 67, 122, 93, 231, 209, 93, 156, 152, 139, 139, 122, 64, 160, 158, 111, 173, 254, 212, 101, 64, 169, 180, 160, 240, 191, 241, 139, 177, 246, 255, 222, 97, 88, 166, 230, 248, 206, 99, 180, 179, 179, 139, 228, 108, 71, 65, 9, 162, 126 }, new byte[] { 1, 119, 233, 172, 254, 59, 99, 207, 242, 173, 16, 50, 240, 115, 102, 234, 193, 154, 237, 42, 19, 219, 151, 51, 105, 227, 43, 101, 24, 212, 233, 67, 191, 218, 121, 173, 154, 65, 158, 45, 140, 132, 93, 156, 175, 49, 253, 134, 210, 142, 31, 44, 87, 121, 14, 220, 149, 203, 52, 70, 27, 50, 232, 35, 117, 55, 156, 225, 244, 145, 102, 177, 183, 111, 104, 94, 202, 150, 3, 48, 230, 140, 199, 50, 101, 229, 218, 110, 100, 65, 29, 66, 5, 250, 186, 12, 52, 53, 155, 42, 122, 11, 191, 119, 86, 230, 102, 58, 161, 72, 199, 166, 45, 250, 224, 73, 29, 201, 57, 200, 121, 138, 20, 104, 204, 108, 31, 248 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5ed3f82a-2b2d-4290-b643-329ab68a948e"), "admin@mail.ru", new byte[] { 92, 133, 34, 45, 213, 248, 242, 219, 42, 175, 237, 231, 59, 22, 166, 16, 140, 71, 10, 254, 82, 229, 102, 9, 160, 16, 146, 185, 183, 153, 99, 175, 133, 154, 255, 58, 34, 1, 177, 93, 115, 96, 182, 193, 90, 205, 44, 255, 76, 91, 167, 67, 225, 96, 127, 72, 25, 14, 206, 231, 90, 191, 239, 169 }, new byte[] { 53, 117, 201, 7, 236, 33, 5, 23, 204, 50, 59, 253, 148, 48, 169, 170, 220, 130, 65, 19, 248, 77, 223, 102, 143, 175, 212, 220, 14, 213, 242, 3, 32, 62, 94, 104, 72, 134, 123, 235, 2, 73, 150, 42, 26, 230, 229, 17, 205, 126, 31, 153, 68, 198, 183, 219, 156, 181, 95, 155, 139, 82, 83, 99, 170, 153, 180, 216, 163, 202, 70, 28, 162, 55, 149, 161, 154, 116, 103, 49, 190, 161, 8, 68, 172, 36, 101, 121, 240, 25, 128, 5, 46, 103, 137, 184, 110, 38, 136, 53, 162, 253, 118, 150, 109, 93, 216, 88, 120, 121, 192, 161, 175, 241, 104, 160, 201, 237, 96, 191, 202, 251, 250, 91, 147, 170, 238, 134 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7a7eac18-1d08-4d91-8395-352901543cd9"), "teacherAdmin@mail.ru", new byte[] { 138, 245, 9, 248, 147, 23, 10, 118, 98, 106, 169, 41, 138, 32, 83, 100, 56, 154, 107, 211, 184, 84, 8, 170, 109, 181, 79, 219, 177, 95, 20, 29, 148, 101, 114, 179, 189, 132, 67, 214, 50, 86, 227, 177, 153, 186, 0, 0, 42, 194, 122, 254, 228, 56, 81, 121, 26, 18, 166, 170, 27, 155, 97, 134 }, new byte[] { 234, 131, 169, 93, 18, 44, 212, 133, 105, 193, 254, 53, 217, 197, 84, 25, 162, 137, 25, 166, 62, 149, 162, 126, 146, 225, 132, 166, 170, 119, 91, 93, 15, 92, 115, 3, 119, 81, 0, 220, 103, 131, 65, 93, 68, 30, 2, 5, 167, 122, 83, 44, 22, 97, 103, 127, 116, 232, 201, 243, 22, 50, 115, 165, 66, 42, 188, 236, 78, 88, 228, 33, 210, 19, 162, 25, 102, 237, 68, 217, 245, 237, 206, 70, 213, 153, 100, 44, 24, 70, 148, 205, 16, 95, 112, 179, 93, 127, 93, 59, 219, 59, 113, 110, 251, 237, 221, 253, 117, 131, 176, 193, 176, 249, 156, 157, 168, 131, 146, 253, 176, 132, 255, 51, 36, 80, 93, 81 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("e35974cc-4719-417a-96ef-958486e2830b"), "teacher@mail.ru", new byte[] { 20, 218, 121, 197, 124, 26, 217, 105, 0, 86, 140, 190, 133, 161, 253, 85, 26, 21, 171, 78, 63, 48, 27, 26, 93, 175, 237, 245, 124, 189, 15, 122, 123, 233, 2, 145, 79, 77, 180, 28, 188, 155, 149, 128, 26, 232, 79, 173, 148, 158, 170, 42, 47, 127, 237, 240, 103, 186, 50, 122, 207, 231, 172, 191 }, new byte[] { 166, 162, 241, 13, 18, 75, 133, 130, 36, 186, 166, 111, 60, 231, 109, 13, 73, 158, 248, 65, 142, 44, 63, 190, 196, 46, 112, 155, 213, 251, 199, 199, 95, 241, 142, 225, 0, 68, 207, 215, 216, 222, 64, 176, 12, 11, 255, 227, 18, 72, 132, 215, 202, 204, 248, 226, 147, 3, 110, 67, 80, 107, 249, 68, 90, 160, 176, 5, 121, 117, 84, 6, 22, 185, 180, 98, 141, 5, 92, 4, 221, 233, 32, 69, 252, 16, 10, 5, 25, 242, 240, 185, 197, 250, 238, 112, 174, 111, 140, 177, 137, 164, 246, 82, 116, 149, 2, 104, 32, 11, 37, 206, 105, 69, 198, 14, 251, 86, 236, 133, 20, 3, 47, 84, 29, 226, 151, 155 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("07aae6f7-7a94-4706-a8dc-fd55c912a96f"), new Guid("462aa5e6-8626-46ba-96b5-26cab9984778") },
                    { new Guid("5c06a1d3-40f0-4400-a6d7-41ebf90bf7ad"), new Guid("7a7eac18-1d08-4d91-8395-352901543cd9") },
                    { new Guid("5c06a1d3-40f0-4400-a6d7-41ebf90bf7ad"), new Guid("e35974cc-4719-417a-96ef-958486e2830b") },
                    { new Guid("6c308ee5-e256-4775-9c6a-22d804b8d038"), new Guid("5ed3f82a-2b2d-4290-b643-329ab68a948e") },
                    { new Guid("6c308ee5-e256-4775-9c6a-22d804b8d038"), new Guid("7a7eac18-1d08-4d91-8395-352901543cd9") }
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
                filter: "\"RoleId\" = '07aae6f7-7a94-4706-a8dc-fd55c912a96f'");

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
