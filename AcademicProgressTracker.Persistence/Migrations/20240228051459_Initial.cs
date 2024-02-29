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
                    LabLessonCount = table.Column<int>(type: "integer", nullable: false),
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
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true)
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
                name: "LabWorks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    MaximumScore = table.Column<decimal>(type: "numeric", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabWorks_Subjects_SubjectId",
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
                name: "LabWorkStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CurrentScore = table.Column<decimal>(type: "numeric", nullable: false),
                    LabWorkId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabWorkStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LabWorkStatuses_LabWorks_LabWorkId",
                        column: x => x.LabWorkId,
                        principalTable: "LabWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LabWorkStatuses_Users_UserId",
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
                    { new Guid("256319a4-e6b0-47e7-b30d-f3226c63dad3"), "Student" },
                    { new Guid("c79e7d56-da96-4387-adf1-9e745422f8b0"), "Teacher" },
                    { new Guid("e1e09171-2eb6-4537-b291-e4c7a871d1c8"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("03ba6e18-d63f-42c2-ae1a-c640436202ad"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("0627a775-0b3e-488c-bbbd-4f958f258a6c"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("0c791647-2060-47e5-9cce-4e63d47ee573"), "История россии", "История России" },
                    { new Guid("3663b5d5-2ad6-493d-955c-b7f918a557bd"), "Самостоятельная работа студента", null },
                    { new Guid("3d9a7907-6530-43b0-8885-36ec7f0a565c"), "Математический аhализ", "Математический анализ" },
                    { new Guid("450a54a8-8f4d-4f39-bde4-db09617aa0c8"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("529022d5-d924-4bed-8fd6-51207428901f"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("8c6313d7-fd0e-4350-9889-a54e048c7b1e"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("a8a0073c-e129-4d29-a49f-fc9a35d95114"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("b8508ac4-86b9-4b3b-aa5c-4020c317f840"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("cf2b0a4e-59db-4c03-b502-eeeffe2c2125"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("d1ff733d-54c1-406e-ae41-40e1ef38d5c7"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("ea651618-bdae-4e2d-a41d-0b6cd205a9e6"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("efee1981-65cc-40b0-983e-1b1f676134aa"), "Субд postgresql", "СУБД PostgreSQL" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("17da9e14-79f3-4525-81a3-0ab476061245"), "teacherAdmin@mail.ru", new byte[] { 238, 66, 228, 19, 99, 82, 67, 205, 94, 32, 28, 168, 36, 192, 252, 77, 87, 225, 218, 13, 197, 73, 9, 142, 138, 52, 184, 170, 231, 6, 79, 147, 186, 44, 125, 82, 90, 124, 97, 110, 187, 40, 134, 141, 159, 233, 131, 17, 163, 131, 181, 3, 100, 135, 123, 201, 176, 22, 92, 177, 245, 176, 146, 80 }, new byte[] { 7, 45, 178, 209, 104, 189, 72, 100, 237, 95, 30, 191, 134, 107, 192, 86, 81, 155, 58, 89, 159, 220, 4, 127, 126, 164, 124, 146, 219, 251, 9, 12, 42, 96, 196, 128, 128, 195, 65, 2, 234, 136, 28, 65, 140, 249, 169, 46, 119, 167, 23, 138, 245, 185, 165, 12, 200, 178, 200, 126, 116, 251, 172, 223, 248, 84, 117, 86, 7, 88, 155, 187, 115, 157, 221, 245, 81, 163, 183, 152, 36, 44, 146, 14, 48, 210, 95, 173, 142, 239, 52, 90, 111, 228, 125, 188, 202, 36, 206, 82, 188, 236, 155, 198, 204, 8, 154, 79, 12, 16, 164, 235, 203, 226, 83, 150, 201, 180, 30, 106, 29, 62, 193, 151, 125, 135, 255, 108 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("290fbd62-8902-4f99-88f5-2f19e5bb6e75"), "teacher@mail.ru", new byte[] { 34, 44, 134, 226, 13, 79, 90, 30, 122, 21, 131, 168, 119, 229, 222, 10, 117, 130, 133, 182, 114, 194, 51, 210, 144, 171, 227, 192, 80, 255, 150, 207, 223, 131, 203, 207, 163, 132, 131, 84, 194, 149, 216, 106, 121, 157, 7, 125, 46, 67, 177, 203, 204, 186, 111, 194, 168, 104, 104, 76, 13, 242, 119, 12 }, new byte[] { 54, 179, 144, 29, 101, 88, 143, 39, 75, 139, 208, 64, 181, 190, 161, 89, 44, 207, 85, 133, 43, 12, 113, 101, 174, 161, 5, 32, 205, 8, 54, 252, 70, 135, 90, 199, 196, 161, 54, 17, 63, 224, 132, 170, 123, 39, 227, 76, 46, 221, 101, 99, 180, 121, 73, 33, 86, 115, 26, 27, 158, 212, 134, 80, 87, 160, 124, 151, 218, 25, 68, 133, 134, 158, 105, 148, 143, 6, 212, 79, 239, 120, 71, 5, 208, 247, 211, 35, 100, 85, 114, 18, 245, 154, 42, 2, 162, 5, 211, 69, 55, 130, 87, 178, 22, 198, 94, 147, 73, 189, 186, 157, 71, 100, 223, 237, 42, 22, 26, 213, 152, 86, 77, 193, 114, 122, 72, 235 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("634398cb-0109-43cc-ba3b-9666bdc17aa2"), "admin@mail.ru", new byte[] { 252, 24, 198, 82, 109, 229, 239, 205, 160, 240, 56, 104, 8, 97, 237, 99, 11, 249, 155, 249, 142, 67, 134, 101, 170, 110, 95, 40, 59, 18, 51, 67, 25, 29, 224, 212, 127, 135, 185, 104, 94, 144, 176, 81, 221, 73, 161, 42, 77, 115, 170, 174, 11, 24, 179, 186, 101, 106, 146, 33, 178, 155, 114, 118 }, new byte[] { 202, 65, 99, 64, 154, 180, 144, 111, 210, 225, 201, 177, 122, 117, 127, 62, 216, 8, 43, 216, 65, 82, 141, 158, 77, 90, 7, 75, 28, 52, 6, 151, 200, 220, 79, 7, 192, 27, 205, 189, 105, 5, 185, 20, 132, 182, 80, 243, 241, 86, 211, 48, 223, 70, 243, 222, 84, 102, 21, 155, 121, 155, 49, 67, 225, 149, 128, 3, 188, 33, 102, 51, 231, 114, 30, 132, 92, 80, 192, 199, 51, 249, 105, 61, 46, 95, 188, 232, 118, 27, 173, 113, 130, 20, 34, 116, 227, 96, 72, 138, 112, 156, 198, 70, 234, 190, 69, 112, 202, 240, 175, 222, 213, 221, 131, 211, 23, 136, 212, 111, 154, 90, 76, 109, 63, 224, 15, 166 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9bf32245-0165-49a5-9562-978f8a3c68dc"), "student@mail.ru", new byte[] { 142, 126, 144, 9, 100, 35, 94, 184, 117, 146, 180, 252, 130, 89, 212, 4, 86, 125, 238, 166, 109, 46, 232, 101, 94, 26, 40, 47, 77, 21, 166, 234, 178, 231, 105, 5, 169, 97, 46, 163, 208, 155, 138, 76, 208, 12, 238, 104, 5, 104, 206, 6, 203, 28, 123, 75, 91, 48, 56, 204, 187, 186, 82, 86 }, new byte[] { 40, 188, 77, 110, 208, 183, 53, 34, 110, 207, 169, 155, 177, 128, 178, 71, 153, 36, 30, 158, 191, 88, 228, 180, 35, 209, 18, 122, 83, 99, 48, 122, 248, 161, 75, 81, 152, 173, 101, 142, 147, 254, 153, 70, 11, 167, 96, 14, 99, 44, 45, 195, 43, 14, 87, 59, 19, 138, 183, 25, 182, 220, 232, 165, 61, 103, 116, 15, 215, 229, 201, 61, 117, 55, 155, 71, 95, 101, 240, 180, 148, 73, 123, 147, 5, 69, 144, 203, 158, 33, 75, 248, 82, 192, 224, 187, 160, 6, 27, 109, 133, 237, 76, 70, 16, 86, 216, 145, 79, 165, 131, 190, 180, 246, 3, 108, 247, 171, 135, 168, 230, 180, 41, 183, 242, 125, 238, 26 }, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("256319a4-e6b0-47e7-b30d-f3226c63dad3"), new Guid("9bf32245-0165-49a5-9562-978f8a3c68dc") },
                    { new Guid("c79e7d56-da96-4387-adf1-9e745422f8b0"), new Guid("17da9e14-79f3-4525-81a3-0ab476061245") },
                    { new Guid("c79e7d56-da96-4387-adf1-9e745422f8b0"), new Guid("290fbd62-8902-4f99-88f5-2f19e5bb6e75") },
                    { new Guid("e1e09171-2eb6-4537-b291-e4c7a871d1c8"), new Guid("17da9e14-79f3-4525-81a3-0ab476061245") },
                    { new Guid("e1e09171-2eb6-4537-b291-e4c7a871d1c8"), new Guid("634398cb-0109-43cc-ba3b-9666bdc17aa2") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabWorkStatuses_LabWorkId",
                table: "LabWorkStatuses",
                column: "LabWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_LabWorkStatuses_UserId",
                table: "LabWorkStatuses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LabWorks_SubjectId",
                table: "LabWorks",
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
                filter: "\"RoleId\" = '256319a4-e6b0-47e7-b30d-f3226c63dad3'");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LabWorkStatuses");

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
                name: "LabWorks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
