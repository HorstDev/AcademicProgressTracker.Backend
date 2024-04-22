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
                    DateTimeOfLastIncreaseCourse = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
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
                    { new Guid("505309d2-e74e-4a77-b2a0-1f6fbc3d6700"), "Admin" },
                    { new Guid("55b4908b-4bb2-460f-b645-efa6fba0815b"), "Student" },
                    { new Guid("c35c64b7-9ac5-4fc9-a7a1-ba5f24f6ae2a"), "Teacher" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("0ed3ba27-ffbc-46bd-b443-0ba98af03de3"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("103b5d81-e21a-4ce9-a12b-bc476f84fa06"), "Математический аhализ", "Математический анализ" },
                    { new Guid("1f9e0807-0753-4812-b283-ad8af52ab159"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("4068e427-b8a7-41f7-a509-6687961ef1b8"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("57e3220c-a06d-48aa-94d4-4034bceed9c3"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("68fbd13b-d2a1-4393-aa4c-5755977b932a"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("7edb0e3f-7da6-46b5-8e63-2302d551e03d"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("a6d51b64-3242-4115-992e-98948f466f27"), "Самостоятельная работа студента", null },
                    { new Guid("a8c54c05-ddfd-4bfa-80d1-8ca2f8962f7a"), "История России", "История России" },
                    { new Guid("a9271e16-9285-4f1c-a9bc-493999c2e660"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("b51bf198-d2cf-43a7-86aa-44fa99df0b69"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("e469f65f-5b48-48eb-a73f-766897e66b87"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("eaaa3b24-98df-43f8-a9ba-8f1e76bd6f19"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("f8d9f6dc-f48a-456e-8969-09a183ebb76b"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("4bc66476-129f-4273-81bc-b8a1decd026d"), "teacher@mail.ru", new byte[] { 137, 12, 152, 97, 117, 114, 126, 14, 154, 91, 245, 105, 114, 2, 66, 157, 157, 252, 150, 31, 111, 10, 119, 69, 172, 254, 116, 80, 6, 179, 46, 191, 225, 121, 95, 111, 252, 211, 23, 220, 187, 94, 11, 163, 47, 219, 152, 21, 34, 109, 91, 217, 76, 126, 209, 81, 242, 164, 35, 3, 14, 207, 105, 143 }, new byte[] { 129, 59, 19, 25, 149, 54, 232, 178, 117, 248, 9, 237, 14, 155, 143, 42, 223, 221, 5, 58, 237, 9, 151, 235, 159, 161, 103, 198, 253, 109, 219, 85, 250, 22, 200, 173, 96, 165, 97, 198, 144, 245, 59, 179, 60, 117, 93, 66, 138, 69, 38, 96, 71, 246, 17, 160, 4, 240, 60, 126, 19, 142, 38, 242, 39, 253, 117, 237, 187, 195, 14, 149, 248, 204, 225, 7, 98, 250, 113, 64, 40, 153, 100, 184, 176, 202, 100, 113, 158, 100, 151, 88, 21, 38, 26, 128, 248, 237, 83, 29, 41, 35, 217, 211, 200, 235, 100, 4, 104, 234, 137, 58, 220, 180, 27, 132, 27, 68, 0, 41, 39, 208, 10, 51, 42, 10, 127, 189 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("59684331-b833-4f00-b058-6a38986d2d73"), "admin@mail.ru", new byte[] { 186, 19, 35, 224, 107, 158, 159, 66, 72, 41, 221, 238, 234, 63, 35, 211, 91, 196, 102, 57, 112, 96, 240, 30, 80, 223, 65, 180, 209, 63, 119, 221, 32, 223, 130, 58, 7, 5, 62, 82, 121, 134, 114, 240, 77, 202, 80, 4, 156, 122, 3, 167, 178, 32, 185, 203, 81, 51, 99, 25, 81, 114, 220, 27 }, new byte[] { 97, 114, 19, 0, 93, 218, 231, 201, 52, 171, 152, 83, 45, 220, 129, 215, 209, 153, 26, 230, 100, 16, 6, 62, 188, 12, 255, 248, 202, 53, 172, 219, 242, 248, 53, 32, 143, 194, 60, 247, 234, 228, 191, 80, 54, 90, 157, 146, 210, 215, 249, 66, 32, 112, 253, 81, 119, 13, 178, 255, 165, 46, 63, 185, 98, 60, 237, 26, 204, 166, 134, 216, 62, 65, 126, 224, 248, 162, 110, 8, 103, 131, 227, 243, 222, 189, 26, 177, 93, 129, 232, 70, 192, 98, 177, 156, 162, 240, 228, 151, 100, 230, 8, 56, 238, 239, 185, 4, 235, 120, 19, 59, 67, 114, 173, 40, 22, 53, 241, 209, 8, 42, 68, 240, 79, 169, 15, 102 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a025a76d-9ed5-403a-b182-6c34173693a6"), "teacherAdmin@mail.ru", new byte[] { 255, 55, 157, 178, 245, 154, 199, 97, 23, 139, 118, 17, 217, 75, 67, 80, 28, 134, 195, 15, 8, 92, 195, 168, 1, 14, 210, 20, 91, 234, 5, 177, 87, 17, 233, 163, 231, 240, 1, 151, 3, 28, 197, 123, 118, 80, 155, 236, 120, 87, 51, 48, 217, 207, 190, 114, 219, 39, 34, 138, 230, 220, 75, 181 }, new byte[] { 210, 172, 215, 36, 79, 45, 193, 60, 174, 22, 66, 71, 90, 66, 113, 77, 80, 60, 189, 239, 196, 120, 121, 230, 84, 46, 91, 99, 76, 255, 144, 161, 26, 135, 48, 254, 193, 251, 111, 244, 208, 33, 71, 222, 31, 130, 170, 109, 175, 210, 244, 91, 38, 116, 242, 33, 28, 195, 87, 156, 234, 36, 82, 219, 197, 137, 66, 40, 89, 56, 155, 184, 28, 118, 126, 86, 96, 46, 17, 177, 142, 76, 248, 3, 156, 136, 207, 49, 103, 74, 76, 107, 208, 4, 205, 175, 247, 21, 46, 215, 35, 220, 93, 82, 249, 132, 241, 53, 106, 223, 135, 108, 151, 97, 108, 83, 171, 123, 12, 35, 235, 73, 130, 129, 234, 249, 40, 131 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("a455c232-bb52-4ce0-b3d2-b592be78282a"), "student@mail.ru", new byte[] { 7, 248, 231, 129, 243, 219, 64, 98, 255, 93, 195, 71, 22, 46, 81, 86, 73, 84, 26, 172, 182, 27, 137, 154, 48, 202, 247, 43, 18, 69, 68, 213, 5, 60, 44, 160, 211, 108, 183, 2, 205, 86, 132, 20, 22, 106, 211, 224, 241, 170, 80, 157, 11, 154, 95, 134, 137, 178, 41, 67, 205, 187, 86, 18 }, new byte[] { 66, 31, 193, 116, 191, 59, 109, 178, 86, 107, 11, 101, 101, 172, 83, 48, 189, 65, 180, 51, 0, 214, 69, 109, 205, 166, 108, 114, 215, 218, 232, 188, 9, 44, 44, 139, 228, 119, 96, 170, 35, 233, 214, 97, 58, 103, 233, 245, 186, 210, 32, 172, 202, 36, 69, 253, 162, 60, 129, 230, 187, 160, 53, 56, 76, 51, 176, 195, 210, 193, 169, 87, 218, 141, 90, 181, 65, 250, 60, 232, 52, 54, 227, 97, 212, 7, 12, 193, 164, 252, 197, 211, 186, 100, 238, 13, 165, 68, 250, 109, 54, 205, 75, 168, 225, 100, 177, 11, 198, 213, 103, 88, 138, 58, 4, 23, 32, 72, 246, 218, 140, 206, 117, 105, 26, 59, 99, 4 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("505309d2-e74e-4a77-b2a0-1f6fbc3d6700"), new Guid("59684331-b833-4f00-b058-6a38986d2d73") },
                    { new Guid("505309d2-e74e-4a77-b2a0-1f6fbc3d6700"), new Guid("a025a76d-9ed5-403a-b182-6c34173693a6") },
                    { new Guid("55b4908b-4bb2-460f-b645-efa6fba0815b"), new Guid("a455c232-bb52-4ce0-b3d2-b592be78282a") },
                    { new Guid("c35c64b7-9ac5-4fc9-a7a1-ba5f24f6ae2a"), new Guid("4bc66476-129f-4273-81bc-b8a1decd026d") },
                    { new Guid("c35c64b7-9ac5-4fc9-a7a1-ba5f24f6ae2a"), new Guid("a025a76d-9ed5-403a-b182-6c34173693a6") }
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
                filter: "\"RoleId\" = '55b4908b-4bb2-460f-b645-efa6fba0815b'");

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
