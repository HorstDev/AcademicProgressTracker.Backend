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
                    { new Guid("22c95dbf-735a-434b-b1be-437445b4dd2e"), "Student" },
                    { new Guid("7102ba69-f7b6-42e4-abc1-fe48aa08cf12"), "Teacher" },
                    { new Guid("cae29eb6-e185-4a06-a83d-0ed776daab40"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("0a4f786c-0025-4849-a12a-7b346c257159"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("0f24d7e6-186a-4330-bcb6-879e0b2ea832"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("3e0a4341-24f8-46d5-bda7-ca06c2c96fc9"), "История России", "История России" },
                    { new Guid("5ce732d5-0990-4a45-a9ed-f8736fbf908b"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("65cf7f00-0d53-4708-95a8-c008cacc39f2"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("6c2d3e58-6163-44f7-9b2a-376e0b8ff4b9"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("ae2d1705-4a2c-406a-a011-a4965f94ca75"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("aeb62342-67a3-47e9-8537-745752beab53"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("bf4134c8-be99-4388-8418-14bbacdbcc1d"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("cacc1b49-f67e-4726-8665-c207da5f3b37"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("d9c5ca00-eb04-44b8-a9a8-8625c28daaea"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("dc8b45c0-d543-4ebb-bd69-bf57d0c8df1c"), "Математический аhализ", "Математический анализ" },
                    { new Guid("e2fe21eb-ede1-41eb-ac2e-3ff2475431c0"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("fc5d6cec-1e55-4b6a-b018-26c423e94e25"), "Самостоятельная работа студента", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("2a68d924-7f89-4e89-9488-66b40dcb2226"), "student@mail.ru", new byte[] { 38, 16, 210, 190, 116, 147, 180, 61, 239, 199, 161, 90, 243, 119, 21, 249, 112, 104, 187, 79, 1, 106, 248, 228, 160, 118, 0, 227, 44, 175, 139, 82, 251, 222, 16, 28, 96, 201, 154, 44, 191, 0, 6, 209, 116, 242, 57, 255, 40, 87, 8, 193, 156, 19, 197, 30, 185, 111, 30, 164, 119, 75, 228, 240 }, new byte[] { 243, 54, 166, 211, 180, 47, 92, 26, 86, 248, 29, 20, 125, 220, 6, 1, 249, 131, 192, 54, 161, 237, 214, 177, 179, 219, 164, 218, 19, 46, 150, 39, 165, 141, 118, 163, 176, 184, 235, 113, 50, 155, 217, 114, 175, 202, 239, 252, 191, 56, 12, 231, 120, 203, 17, 65, 115, 131, 32, 20, 12, 118, 178, 253, 232, 165, 58, 147, 255, 105, 223, 144, 125, 98, 183, 12, 199, 33, 37, 254, 127, 212, 244, 67, 46, 166, 97, 248, 147, 60, 41, 153, 219, 15, 212, 132, 168, 49, 214, 15, 109, 16, 2, 254, 125, 63, 146, 131, 102, 212, 72, 61, 136, 6, 243, 26, 109, 33, 197, 31, 85, 190, 147, 86, 60, 78, 96, 131 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6269416f-27ee-4176-8011-20dd82129101"), "teacher@mail.ru", new byte[] { 128, 178, 13, 4, 227, 43, 62, 132, 14, 8, 225, 37, 124, 151, 141, 111, 245, 222, 255, 49, 110, 50, 133, 190, 9, 150, 69, 229, 24, 84, 208, 214, 127, 21, 35, 174, 44, 50, 196, 141, 68, 139, 112, 183, 159, 101, 232, 54, 53, 136, 186, 198, 27, 122, 96, 195, 87, 48, 175, 189, 77, 43, 189, 113 }, new byte[] { 58, 2, 81, 141, 227, 88, 60, 238, 134, 2, 53, 168, 178, 138, 15, 184, 7, 170, 1, 244, 193, 106, 238, 186, 123, 210, 59, 142, 64, 121, 207, 188, 103, 33, 168, 20, 164, 9, 32, 38, 190, 25, 193, 1, 73, 185, 39, 218, 84, 140, 241, 127, 225, 26, 208, 91, 243, 54, 122, 240, 37, 217, 182, 47, 53, 221, 200, 165, 37, 206, 169, 154, 100, 73, 116, 82, 62, 240, 227, 245, 90, 77, 225, 66, 15, 208, 225, 186, 20, 31, 171, 49, 133, 101, 251, 44, 228, 175, 251, 119, 226, 162, 61, 119, 40, 218, 27, 122, 207, 89, 92, 104, 247, 122, 162, 193, 88, 207, 89, 40, 155, 13, 181, 80, 204, 163, 56, 192 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("76a229cb-fcad-4453-9325-409be0c798fb"), "teacherAdmin@mail.ru", new byte[] { 18, 99, 223, 62, 165, 6, 242, 222, 255, 74, 151, 80, 99, 220, 61, 152, 234, 162, 2, 173, 104, 74, 192, 244, 218, 123, 162, 47, 70, 227, 206, 193, 156, 8, 161, 40, 244, 227, 69, 41, 59, 201, 163, 88, 35, 104, 206, 50, 93, 248, 12, 177, 235, 32, 120, 246, 164, 159, 167, 132, 169, 235, 185, 232 }, new byte[] { 2, 42, 175, 153, 104, 125, 191, 105, 87, 116, 65, 115, 143, 249, 214, 113, 63, 102, 209, 135, 162, 170, 32, 120, 74, 65, 249, 219, 121, 211, 61, 147, 27, 59, 6, 231, 188, 185, 63, 31, 148, 152, 150, 116, 1, 5, 41, 215, 155, 83, 26, 111, 74, 186, 131, 49, 142, 148, 222, 241, 239, 214, 170, 77, 75, 28, 157, 238, 213, 78, 208, 91, 240, 253, 177, 41, 201, 234, 241, 169, 61, 234, 40, 25, 56, 102, 89, 160, 90, 14, 60, 237, 8, 124, 229, 136, 245, 250, 148, 168, 22, 107, 173, 124, 250, 172, 115, 185, 247, 1, 238, 157, 58, 75, 191, 202, 59, 181, 181, 196, 50, 5, 247, 71, 135, 124, 197, 117 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9c9d1d90-5a6a-4882-816d-f3b8a3fc528c"), "admin@mail.ru", new byte[] { 167, 77, 132, 138, 82, 46, 169, 84, 228, 142, 84, 48, 236, 156, 64, 18, 10, 104, 226, 1, 175, 15, 1, 24, 226, 154, 93, 213, 92, 138, 141, 129, 62, 182, 120, 254, 189, 63, 64, 140, 163, 99, 19, 117, 232, 15, 37, 231, 93, 239, 22, 206, 218, 117, 172, 195, 58, 148, 113, 210, 53, 18, 218, 216 }, new byte[] { 161, 11, 25, 209, 163, 112, 69, 43, 39, 187, 186, 31, 235, 35, 235, 197, 156, 134, 76, 129, 47, 74, 124, 43, 182, 196, 11, 178, 112, 21, 57, 201, 167, 31, 58, 54, 213, 66, 177, 234, 123, 141, 147, 34, 237, 17, 18, 137, 186, 172, 109, 91, 115, 45, 29, 132, 11, 146, 201, 213, 156, 149, 213, 33, 189, 49, 49, 235, 245, 75, 164, 248, 52, 30, 128, 235, 99, 136, 219, 4, 62, 140, 252, 69, 28, 88, 62, 216, 115, 70, 169, 196, 65, 249, 75, 124, 141, 93, 199, 134, 209, 255, 129, 50, 71, 30, 68, 248, 6, 247, 108, 2, 96, 223, 88, 26, 166, 164, 138, 235, 148, 229, 158, 206, 87, 160, 211, 201 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("22c95dbf-735a-434b-b1be-437445b4dd2e"), new Guid("2a68d924-7f89-4e89-9488-66b40dcb2226") },
                    { new Guid("7102ba69-f7b6-42e4-abc1-fe48aa08cf12"), new Guid("6269416f-27ee-4176-8011-20dd82129101") },
                    { new Guid("7102ba69-f7b6-42e4-abc1-fe48aa08cf12"), new Guid("76a229cb-fcad-4453-9325-409be0c798fb") },
                    { new Guid("cae29eb6-e185-4a06-a83d-0ed776daab40"), new Guid("76a229cb-fcad-4453-9325-409be0c798fb") },
                    { new Guid("cae29eb6-e185-4a06-a83d-0ed776daab40"), new Guid("9c9d1d90-5a6a-4882-816d-f3b8a3fc528c") }
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
                filter: "\"RoleId\" = '22c95dbf-735a-434b-b1be-437445b4dd2e'");

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
