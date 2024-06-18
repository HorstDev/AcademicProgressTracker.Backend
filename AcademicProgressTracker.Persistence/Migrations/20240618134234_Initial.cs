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
                    { new Guid("02a6d8a0-4c58-4d3d-84c6-46a8e2ee86f0"), "Student" },
                    { new Guid("0349835f-2688-4a03-b65e-d750d3bbfa65"), "Teacher" },
                    { new Guid("9fd61bdb-6405-433f-b343-1c85a2df763b"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("1fb63a85-9311-4306-94f5-57eb4a86f7ca"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("2889cb9b-ffad-41d1-b33d-cd40c684e462"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("3d03ca51-245f-411e-b853-b189e15d5d63"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("3d15e34e-dcb3-46e1-884e-c1bcbcdb3d05"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("42936028-1ff7-4f9a-8207-be8b1a38fa6a"), "Математический аhализ", "Математический анализ" },
                    { new Guid("4a302070-bc82-4c03-87f9-700265d22eda"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("50231d4f-1c34-4eb0-90ae-69bd14ec17dd"), "История России", "История России" },
                    { new Guid("69f31a74-0dca-4889-9a81-714c021ebd82"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("704e80f7-85a1-4ef6-b5a0-435ea0ad8a16"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("94f59285-1c79-48f8-904d-45f5be7d1a77"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("a00d96f2-2fce-4361-beaa-fb8bc39894f6"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("a6805b38-2f9f-4c06-8aff-282b354756a0"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("b9de0bf4-8d44-4bc1-b0f9-9113397a0a6a"), "Самостоятельная работа студента", null },
                    { new Guid("d782480c-9316-49d0-ab5a-9c62172d6ac4"), "Субд postgresql", "СУБД PostgreSQL" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("180a99e1-9beb-46ce-9ac8-c155973df887"), "student@mail.ru", new byte[] { 145, 191, 146, 76, 177, 253, 157, 135, 206, 168, 116, 108, 195, 130, 120, 181, 171, 102, 8, 62, 90, 191, 170, 106, 51, 196, 93, 81, 90, 139, 131, 63, 134, 115, 142, 179, 231, 163, 44, 116, 120, 251, 251, 244, 11, 130, 128, 224, 66, 124, 12, 182, 204, 44, 215, 182, 60, 163, 37, 203, 167, 160, 121, 238 }, new byte[] { 130, 94, 144, 7, 182, 166, 122, 41, 217, 38, 72, 175, 2, 153, 68, 42, 207, 192, 79, 241, 222, 124, 147, 226, 47, 71, 8, 12, 242, 203, 231, 126, 214, 48, 33, 103, 162, 41, 28, 37, 64, 193, 250, 100, 49, 158, 3, 148, 219, 154, 10, 63, 136, 247, 91, 150, 251, 21, 80, 183, 104, 223, 29, 4, 146, 28, 177, 39, 197, 88, 128, 241, 71, 152, 45, 10, 184, 209, 170, 199, 195, 253, 161, 111, 161, 14, 108, 211, 161, 219, 27, 7, 96, 41, 107, 90, 207, 161, 218, 76, 34, 218, 144, 253, 200, 65, 186, 73, 251, 153, 4, 210, 95, 182, 198, 10, 75, 156, 196, 1, 150, 145, 152, 166, 176, 1, 254, 139 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1fd514f3-be8f-40d6-a97d-7968bc3c1cf0"), "teacherAdmin@mail.ru", new byte[] { 212, 169, 102, 26, 102, 103, 154, 40, 167, 16, 114, 196, 193, 175, 164, 121, 198, 139, 243, 156, 72, 71, 66, 157, 4, 14, 205, 104, 57, 232, 142, 15, 102, 138, 134, 192, 76, 11, 153, 122, 222, 112, 48, 78, 250, 227, 233, 79, 173, 181, 89, 173, 251, 182, 8, 146, 101, 58, 138, 203, 132, 113, 137, 212 }, new byte[] { 153, 30, 178, 213, 242, 46, 160, 104, 56, 176, 90, 28, 158, 132, 79, 77, 44, 22, 119, 127, 230, 148, 250, 7, 179, 95, 194, 152, 101, 189, 108, 19, 213, 156, 137, 131, 125, 251, 50, 59, 29, 143, 119, 8, 175, 161, 109, 232, 137, 62, 240, 118, 70, 188, 10, 51, 213, 80, 166, 100, 239, 186, 249, 183, 198, 94, 27, 68, 219, 111, 41, 144, 220, 25, 190, 6, 147, 59, 90, 196, 91, 95, 183, 189, 137, 161, 211, 109, 199, 250, 177, 44, 121, 232, 156, 20, 216, 43, 182, 73, 33, 130, 180, 178, 130, 245, 247, 236, 241, 210, 77, 163, 158, 236, 168, 243, 135, 199, 199, 11, 81, 253, 219, 5, 0, 145, 116, 182 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("587b4db9-4997-42a3-9040-394f2edab617"), "teacher@mail.ru", new byte[] { 232, 126, 132, 118, 224, 2, 83, 93, 39, 171, 124, 43, 97, 131, 110, 134, 28, 205, 38, 180, 83, 61, 232, 63, 128, 118, 154, 69, 243, 88, 173, 95, 219, 30, 11, 66, 108, 210, 248, 155, 170, 208, 0, 203, 245, 182, 255, 102, 210, 243, 170, 150, 221, 10, 25, 250, 223, 135, 77, 169, 176, 227, 92, 40 }, new byte[] { 223, 239, 113, 200, 110, 61, 159, 214, 32, 121, 146, 83, 130, 96, 60, 47, 189, 242, 28, 15, 219, 47, 124, 226, 68, 218, 170, 95, 152, 155, 123, 127, 106, 9, 254, 201, 7, 150, 5, 2, 227, 196, 81, 194, 90, 205, 38, 250, 227, 220, 65, 206, 141, 246, 117, 128, 183, 196, 124, 90, 203, 177, 37, 67, 140, 23, 97, 241, 239, 44, 163, 227, 32, 36, 74, 118, 6, 203, 161, 88, 135, 229, 88, 2, 213, 175, 117, 33, 81, 160, 19, 194, 69, 175, 200, 150, 65, 43, 40, 56, 192, 166, 15, 7, 153, 32, 137, 230, 105, 230, 158, 150, 54, 61, 126, 19, 171, 180, 193, 96, 212, 67, 218, 219, 201, 96, 91, 255 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("f5ee4cc9-cecf-43d9-9c38-3c5684374580"), "admin@mail.ru", new byte[] { 13, 157, 58, 111, 136, 218, 60, 114, 197, 35, 248, 128, 151, 230, 123, 207, 132, 38, 138, 191, 40, 163, 200, 251, 3, 123, 115, 239, 216, 253, 252, 117, 194, 40, 35, 151, 55, 147, 254, 212, 236, 128, 216, 85, 22, 25, 131, 28, 225, 141, 146, 110, 235, 8, 121, 223, 233, 111, 134, 203, 213, 90, 204, 250 }, new byte[] { 99, 98, 21, 54, 174, 0, 140, 122, 84, 85, 111, 9, 197, 20, 119, 131, 41, 190, 197, 72, 126, 17, 236, 33, 44, 251, 177, 179, 50, 21, 237, 48, 66, 148, 40, 39, 76, 144, 1, 211, 249, 132, 44, 86, 54, 10, 2, 152, 80, 56, 86, 215, 217, 142, 26, 182, 244, 39, 171, 43, 142, 254, 56, 62, 247, 226, 9, 42, 217, 44, 119, 103, 148, 8, 228, 176, 81, 237, 130, 24, 199, 60, 30, 172, 15, 77, 192, 237, 66, 20, 123, 56, 99, 198, 204, 77, 76, 33, 97, 207, 49, 66, 45, 118, 31, 72, 41, 102, 156, 28, 81, 56, 191, 29, 147, 203, 112, 172, 48, 137, 244, 156, 111, 8, 174, 16, 196, 37 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("02a6d8a0-4c58-4d3d-84c6-46a8e2ee86f0"), new Guid("180a99e1-9beb-46ce-9ac8-c155973df887") },
                    { new Guid("0349835f-2688-4a03-b65e-d750d3bbfa65"), new Guid("1fd514f3-be8f-40d6-a97d-7968bc3c1cf0") },
                    { new Guid("0349835f-2688-4a03-b65e-d750d3bbfa65"), new Guid("587b4db9-4997-42a3-9040-394f2edab617") },
                    { new Guid("9fd61bdb-6405-433f-b343-1c85a2df763b"), new Guid("1fd514f3-be8f-40d6-a97d-7968bc3c1cf0") },
                    { new Guid("9fd61bdb-6405-433f-b343-1c85a2df763b"), new Guid("f5ee4cc9-cecf-43d9-9c38-3c5684374580") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_Name",
                table: "Groups",
                column: "Name",
                unique: true);

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
                filter: "\"RoleId\" = '02a6d8a0-4c58-4d3d-84c6-46a8e2ee86f0'");

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
