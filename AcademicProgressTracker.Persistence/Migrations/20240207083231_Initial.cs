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
                    Course = table.Column<int>(type: "integer", nullable: false),
                    YearCreated = table.Column<int>(type: "integer", nullable: false)
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: false),
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
                    MaximumScore = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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
                    CurrentScore = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
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
                table: "Groups",
                columns: new[] { "Id", "Course", "Name", "YearCreated" },
                values: new object[,]
                {
                    { new Guid("95724443-5363-4c20-a262-da380d468e55"), 4, "ДИПРБ", 2020 },
                    { new Guid("e8d0f771-d165-45c7-8f01-18c7ed92361b"), 4, "ДИИЭБ", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2476f1d7-44f6-4600-8f43-7bd0d0d94c13"), "Teacher" },
                    { new Guid("8858a359-c8cd-41c0-a5c8-351262ea7cb6"), "Student" },
                    { new Guid("8d6833f3-7a48-4397-84fd-8241e160324e"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("5cc3730e-0156-4222-aec2-047c10143f2e"), "student@mail.ru", new byte[] { 54, 244, 32, 176, 192, 126, 249, 183, 59, 144, 0, 198, 34, 88, 165, 9, 240, 162, 89, 158, 250, 87, 178, 106, 170, 210, 40, 115, 155, 79, 166, 72, 141, 228, 182, 128, 113, 180, 253, 14, 196, 20, 221, 55, 210, 136, 92, 179, 76, 72, 8, 31, 191, 17, 111, 50, 68, 30, 128, 5, 82, 9, 127, 92 }, new byte[] { 162, 6, 43, 204, 19, 222, 67, 56, 233, 25, 220, 80, 32, 181, 130, 124, 252, 113, 37, 3, 209, 202, 89, 147, 217, 93, 49, 20, 147, 176, 178, 85, 139, 254, 244, 226, 132, 100, 89, 129, 46, 167, 209, 114, 118, 52, 159, 170, 22, 174, 158, 120, 29, 63, 1, 250, 246, 94, 5, 237, 78, 109, 219, 80, 20, 4, 52, 253, 172, 154, 25, 56, 37, 238, 175, 20, 172, 124, 222, 148, 38, 0, 209, 119, 103, 247, 22, 52, 104, 223, 142, 252, 95, 91, 51, 34, 187, 110, 69, 38, 16, 134, 161, 124, 57, 19, 17, 17, 69, 234, 68, 225, 186, 175, 56, 43, 78, 78, 11, 34, 12, 188, 231, 118, 133, 251, 212, 45 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("92662ddd-621c-49b8-9e59-9b8cfdc93dce"), "teacher@mail.ru", new byte[] { 254, 18, 255, 204, 97, 87, 247, 85, 174, 40, 123, 16, 44, 204, 33, 93, 92, 41, 100, 128, 255, 234, 105, 182, 8, 154, 233, 87, 252, 36, 87, 222, 62, 203, 205, 249, 64, 106, 238, 70, 183, 96, 167, 41, 88, 96, 243, 145, 74, 138, 126, 28, 156, 18, 32, 69, 132, 200, 20, 101, 162, 171, 197, 107 }, new byte[] { 11, 16, 172, 38, 227, 10, 48, 202, 91, 204, 120, 229, 65, 212, 126, 124, 225, 166, 231, 255, 24, 155, 249, 108, 79, 185, 4, 144, 231, 12, 82, 24, 213, 202, 48, 169, 206, 93, 79, 171, 123, 197, 221, 216, 215, 194, 58, 101, 146, 189, 204, 76, 148, 232, 156, 157, 116, 185, 84, 223, 196, 219, 131, 95, 181, 72, 129, 239, 16, 230, 181, 255, 173, 63, 90, 132, 215, 145, 196, 104, 11, 239, 99, 31, 9, 85, 246, 89, 178, 240, 136, 36, 3, 218, 49, 219, 40, 156, 43, 134, 87, 113, 87, 78, 50, 54, 77, 172, 176, 29, 111, 3, 189, 169, 3, 214, 160, 172, 128, 173, 38, 62, 182, 11, 29, 254, 50, 220 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("d3f6db69-f2f1-45dc-88c6-02b6189866e1"), "teacherAdmin@mail.ru", new byte[] { 215, 169, 96, 116, 17, 146, 197, 122, 83, 119, 214, 16, 104, 230, 36, 65, 10, 76, 112, 74, 211, 149, 127, 217, 204, 49, 149, 79, 128, 180, 105, 52, 4, 86, 96, 122, 66, 32, 104, 138, 80, 234, 137, 213, 93, 110, 246, 41, 194, 119, 191, 250, 193, 115, 223, 225, 16, 114, 146, 164, 187, 129, 56, 108 }, new byte[] { 23, 25, 245, 28, 114, 191, 48, 51, 82, 234, 51, 77, 76, 248, 234, 191, 169, 206, 82, 68, 224, 230, 49, 184, 217, 147, 89, 195, 201, 240, 74, 88, 5, 123, 35, 50, 47, 98, 15, 33, 238, 113, 133, 205, 196, 27, 225, 189, 186, 203, 92, 85, 133, 166, 102, 202, 103, 114, 236, 109, 23, 185, 220, 249, 99, 237, 93, 233, 150, 235, 128, 228, 225, 11, 165, 230, 99, 14, 181, 172, 1, 20, 73, 251, 182, 21, 99, 77, 214, 59, 159, 186, 19, 56, 234, 144, 30, 242, 113, 64, 44, 115, 23, 11, 237, 210, 242, 231, 64, 38, 92, 199, 54, 250, 58, 163, 92, 87, 220, 241, 236, 9, 165, 183, 67, 249, 205, 53 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("dcb54be9-9121-42bc-a3f5-57823b133027"), "admin@mail.ru", new byte[] { 9, 56, 7, 7, 169, 242, 179, 42, 84, 36, 225, 240, 138, 187, 160, 5, 55, 186, 217, 226, 12, 55, 141, 82, 86, 208, 58, 196, 157, 161, 244, 195, 121, 248, 60, 253, 183, 107, 65, 126, 189, 151, 232, 18, 74, 53, 144, 93, 204, 35, 132, 114, 91, 250, 138, 223, 91, 54, 156, 172, 74, 95, 75, 45 }, new byte[] { 132, 87, 226, 86, 91, 246, 221, 197, 109, 30, 204, 197, 107, 20, 222, 32, 85, 3, 30, 228, 108, 228, 82, 23, 185, 189, 138, 15, 58, 255, 40, 192, 117, 143, 111, 19, 5, 249, 86, 3, 136, 255, 56, 33, 241, 52, 56, 243, 178, 165, 255, 164, 59, 159, 61, 6, 254, 113, 209, 178, 88, 9, 237, 17, 201, 221, 190, 186, 115, 185, 163, 96, 117, 104, 17, 134, 65, 191, 208, 165, 188, 18, 178, 144, 36, 128, 113, 3, 65, 125, 1, 61, 95, 178, 239, 188, 143, 239, 244, 18, 250, 117, 98, 214, 75, 254, 145, 102, 17, 204, 172, 140, 17, 89, 225, 74, 94, 251, 206, 65, 170, 66, 162, 124, 73, 129, 84, 195 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "GroupId", "Name", "Semester" },
                values: new object[] { new Guid("da792817-5aa6-4cef-ac33-ed9c55b8529f"), new Guid("95724443-5363-4c20-a262-da380d468e55"), "СУБД PostgreSQL", 0 });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "GroupId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("13fe4f0d-4397-4eb2-bffc-98ced8f53b4f"), new Guid("95724443-5363-4c20-a262-da380d468e55"), new Guid("2476f1d7-44f6-4600-8f43-7bd0d0d94c13"), new Guid("92662ddd-621c-49b8-9e59-9b8cfdc93dce") },
                    { new Guid("1b54e61c-d95a-4f7c-abd8-e2c5c4fb49e7"), new Guid("95724443-5363-4c20-a262-da380d468e55"), new Guid("8858a359-c8cd-41c0-a5c8-351262ea7cb6"), new Guid("5cc3730e-0156-4222-aec2-047c10143f2e") },
                    { new Guid("e821a7e6-5e3e-4b15-8628-f905073f782f"), new Guid("e8d0f771-d165-45c7-8f01-18c7ed92361b"), new Guid("2476f1d7-44f6-4600-8f43-7bd0d0d94c13"), new Guid("92662ddd-621c-49b8-9e59-9b8cfdc93dce") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2476f1d7-44f6-4600-8f43-7bd0d0d94c13"), new Guid("92662ddd-621c-49b8-9e59-9b8cfdc93dce") },
                    { new Guid("2476f1d7-44f6-4600-8f43-7bd0d0d94c13"), new Guid("d3f6db69-f2f1-45dc-88c6-02b6189866e1") },
                    { new Guid("8858a359-c8cd-41c0-a5c8-351262ea7cb6"), new Guid("5cc3730e-0156-4222-aec2-047c10143f2e") },
                    { new Guid("8d6833f3-7a48-4397-84fd-8241e160324e"), new Guid("d3f6db69-f2f1-45dc-88c6-02b6189866e1") },
                    { new Guid("8d6833f3-7a48-4397-84fd-8241e160324e"), new Guid("dcb54be9-9121-42bc-a3f5-57823b133027") }
                });

            migrationBuilder.InsertData(
                table: "LabWorks",
                columns: new[] { "Id", "MaximumScore", "Number", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("111b725b-f942-49db-8f4b-d767d43f561c"), 10m, 3, new Guid("da792817-5aa6-4cef-ac33-ed9c55b8529f") },
                    { new Guid("a7278cef-91e0-40a2-871b-a45a16952550"), 10m, 4, new Guid("da792817-5aa6-4cef-ac33-ed9c55b8529f") },
                    { new Guid("a943e750-0668-4191-8a5c-f5d9f6e64c2b"), 10m, 2, new Guid("da792817-5aa6-4cef-ac33-ed9c55b8529f") },
                    { new Guid("eb83f7f5-e212-409f-816a-decbb7dc52b9"), 10m, 1, new Guid("da792817-5aa6-4cef-ac33-ed9c55b8529f") }
                });

            migrationBuilder.InsertData(
                table: "TeacherSubject",
                columns: new[] { "SubjectId", "UserId" },
                values: new object[] { new Guid("da792817-5aa6-4cef-ac33-ed9c55b8529f"), new Guid("92662ddd-621c-49b8-9e59-9b8cfdc93dce") });

            migrationBuilder.InsertData(
                table: "LabWorkStatuses",
                columns: new[] { "Id", "CurrentScore", "IsCompleted", "LabWorkId", "UserId" },
                values: new object[,]
                {
                    { new Guid("06428e4c-3775-4df7-9204-dc59b3b2ef80"), 0m, false, new Guid("eb83f7f5-e212-409f-816a-decbb7dc52b9"), new Guid("5cc3730e-0156-4222-aec2-047c10143f2e") },
                    { new Guid("523b7b93-ed33-43ed-b033-8339739323fd"), 0m, false, new Guid("a943e750-0668-4191-8a5c-f5d9f6e64c2b"), new Guid("5cc3730e-0156-4222-aec2-047c10143f2e") },
                    { new Guid("54fbaafa-aab0-444e-88d3-edc9627dc098"), 0m, false, new Guid("111b725b-f942-49db-8f4b-d767d43f561c"), new Guid("5cc3730e-0156-4222-aec2-047c10143f2e") },
                    { new Guid("6e04227a-4d98-420f-8f4f-2419103ccb09"), 0m, false, new Guid("a7278cef-91e0-40a2-871b-a45a16952550"), new Guid("5cc3730e-0156-4222-aec2-047c10143f2e") }
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
                filter: "\"RoleId\" = '8858a359-c8cd-41c0-a5c8-351262ea7cb6'");

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
