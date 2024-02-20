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
                    YearCreated = table.Column<int>(type: "integer", nullable: false),
                    CurriculumExcelDocument = table.Column<byte[]>(type: "bytea", nullable: true)
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
                columns: new[] { "Id", "Course", "CurriculumExcelDocument", "Name", "YearCreated" },
                values: new object[,]
                {
                    { new Guid("b44185a6-d896-4085-a48e-daaee4b9d3ec"), 4, null, "ДИПРБ", 2020 },
                    { new Guid("c1afa60f-c6aa-46b0-b97e-5011c02d3ba4"), 4, null, "ДИИЭБ", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), "Teacher" },
                    { new Guid("56ec748e-6719-4534-8491-b332582b95ad"), "Admin" },
                    { new Guid("7de8c887-8e3d-42e3-8de2-ec13b0c52f01"), "Student" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("1b73e408-2fe1-455c-b9e5-2a3dc81481aa"), "admin@mail.ru", new byte[] { 71, 18, 82, 136, 39, 215, 102, 80, 252, 23, 213, 43, 43, 60, 96, 151, 190, 80, 254, 56, 5, 196, 156, 157, 32, 238, 46, 130, 156, 95, 11, 75, 7, 80, 139, 157, 237, 3, 191, 214, 173, 188, 90, 109, 45, 149, 128, 34, 201, 50, 138, 210, 50, 96, 124, 151, 223, 186, 101, 27, 4, 149, 115, 173 }, new byte[] { 132, 52, 209, 212, 224, 219, 108, 77, 52, 38, 72, 168, 209, 244, 125, 186, 182, 41, 94, 174, 86, 209, 188, 232, 110, 115, 18, 122, 150, 82, 161, 83, 4, 189, 69, 54, 101, 43, 103, 99, 115, 146, 100, 117, 132, 13, 29, 212, 199, 209, 111, 141, 84, 31, 21, 79, 33, 78, 130, 35, 25, 8, 167, 159, 32, 132, 242, 124, 209, 20, 252, 18, 82, 119, 189, 55, 132, 114, 156, 222, 240, 158, 127, 148, 120, 26, 138, 150, 81, 65, 15, 246, 166, 52, 57, 62, 254, 14, 184, 130, 53, 12, 80, 140, 25, 8, 119, 229, 78, 54, 127, 61, 9, 141, 225, 76, 122, 142, 27, 133, 171, 145, 194, 127, 210, 158, 45, 63 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("24b3720e-a08f-4568-9498-237ceecc41f0"), "teacherAdmin@mail.ru", new byte[] { 7, 106, 129, 11, 191, 44, 16, 205, 62, 97, 13, 30, 8, 25, 49, 132, 68, 220, 192, 50, 253, 208, 160, 148, 167, 149, 65, 159, 83, 121, 120, 59, 194, 206, 38, 57, 152, 69, 81, 176, 249, 194, 115, 53, 27, 177, 188, 67, 73, 26, 242, 232, 219, 148, 24, 240, 13, 142, 44, 13, 21, 55, 86, 21 }, new byte[] { 172, 117, 179, 152, 39, 48, 75, 44, 17, 193, 29, 111, 251, 252, 242, 51, 199, 211, 238, 117, 138, 64, 99, 99, 71, 81, 202, 119, 44, 232, 67, 35, 110, 68, 65, 52, 106, 46, 108, 45, 192, 233, 69, 18, 151, 59, 119, 66, 85, 150, 250, 94, 251, 99, 169, 187, 75, 120, 248, 59, 24, 172, 40, 52, 151, 213, 24, 68, 194, 67, 185, 175, 164, 184, 60, 11, 67, 125, 131, 253, 86, 56, 173, 100, 176, 199, 63, 185, 132, 56, 153, 82, 4, 132, 91, 93, 184, 101, 112, 32, 124, 206, 125, 125, 46, 211, 20, 85, 180, 255, 209, 99, 254, 145, 142, 88, 65, 203, 166, 145, 238, 207, 10, 137, 102, 213, 93, 203 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8"), "student@mail.ru", new byte[] { 215, 217, 30, 116, 67, 171, 20, 76, 191, 48, 207, 75, 182, 232, 41, 197, 197, 219, 132, 120, 195, 28, 208, 25, 59, 103, 187, 223, 115, 104, 241, 65, 250, 89, 154, 182, 42, 149, 125, 6, 144, 52, 177, 206, 210, 217, 210, 63, 8, 186, 113, 110, 235, 67, 116, 1, 124, 85, 229, 144, 203, 62, 83, 172 }, new byte[] { 188, 46, 206, 23, 130, 87, 42, 136, 214, 142, 136, 60, 254, 190, 61, 166, 153, 27, 182, 50, 255, 142, 90, 233, 23, 31, 162, 62, 171, 117, 159, 2, 166, 159, 97, 12, 162, 0, 51, 37, 185, 183, 216, 100, 129, 201, 30, 171, 219, 77, 177, 238, 97, 140, 234, 56, 181, 69, 149, 167, 234, 120, 117, 60, 15, 186, 1, 200, 185, 22, 103, 217, 142, 84, 84, 158, 79, 42, 76, 48, 174, 178, 181, 18, 168, 161, 140, 191, 86, 137, 178, 16, 46, 203, 244, 83, 204, 104, 130, 250, 164, 130, 59, 10, 87, 207, 212, 18, 101, 60, 252, 167, 73, 134, 74, 119, 47, 15, 53, 92, 226, 156, 9, 84, 190, 110, 185, 203 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece"), "teacher@mail.ru", new byte[] { 239, 90, 8, 91, 23, 50, 123, 97, 130, 190, 126, 3, 106, 33, 77, 60, 12, 25, 32, 136, 196, 153, 169, 218, 45, 199, 194, 217, 252, 83, 10, 9, 197, 30, 152, 54, 64, 189, 227, 70, 232, 187, 78, 133, 222, 80, 222, 18, 135, 215, 54, 253, 0, 226, 168, 102, 148, 112, 66, 254, 55, 22, 114, 143 }, new byte[] { 51, 232, 78, 82, 155, 225, 98, 98, 153, 247, 103, 118, 133, 44, 214, 91, 252, 111, 214, 57, 59, 249, 117, 232, 81, 238, 144, 24, 223, 79, 31, 230, 208, 103, 13, 123, 67, 148, 253, 213, 248, 254, 22, 195, 50, 237, 93, 146, 17, 164, 69, 91, 1, 37, 139, 231, 139, 68, 73, 120, 165, 200, 17, 80, 27, 24, 72, 72, 82, 246, 20, 144, 39, 116, 220, 16, 0, 167, 226, 132, 117, 181, 133, 70, 123, 37, 45, 238, 36, 61, 249, 95, 161, 94, 223, 47, 157, 207, 154, 208, 24, 163, 145, 108, 28, 176, 128, 194, 22, 233, 249, 125, 69, 125, 175, 64, 119, 27, 56, 96, 71, 116, 62, 253, 75, 170, 139, 67 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "GroupId", "Name", "Semester" },
                values: new object[] { new Guid("6f5d821a-f421-459b-8e6d-171b90d80444"), new Guid("b44185a6-d896-4085-a48e-daaee4b9d3ec"), "СУБД PostgreSQL", 0 });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "GroupId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("573cf133-8c57-410f-b5dd-3dcf2d359d4e"), new Guid("b44185a6-d896-4085-a48e-daaee4b9d3ec"), new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece") },
                    { new Guid("b52c035c-495a-4f36-a1bf-61b06b055977"), new Guid("b44185a6-d896-4085-a48e-daaee4b9d3ec"), new Guid("7de8c887-8e3d-42e3-8de2-ec13b0c52f01"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") },
                    { new Guid("e30817d5-4173-46aa-855d-fa267e574aee"), new Guid("c1afa60f-c6aa-46b0-b97e-5011c02d3ba4"), new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), new Guid("24b3720e-a08f-4568-9498-237ceecc41f0") },
                    { new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece") },
                    { new Guid("56ec748e-6719-4534-8491-b332582b95ad"), new Guid("1b73e408-2fe1-455c-b9e5-2a3dc81481aa") },
                    { new Guid("56ec748e-6719-4534-8491-b332582b95ad"), new Guid("24b3720e-a08f-4568-9498-237ceecc41f0") },
                    { new Guid("7de8c887-8e3d-42e3-8de2-ec13b0c52f01"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") }
                });

            migrationBuilder.InsertData(
                table: "LabWorks",
                columns: new[] { "Id", "MaximumScore", "Number", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("5feed908-153c-4729-bbb5-09ec65543040"), 10m, 2, new Guid("6f5d821a-f421-459b-8e6d-171b90d80444") },
                    { new Guid("98048840-72eb-4f15-8bfe-162904df1d04"), 10m, 1, new Guid("6f5d821a-f421-459b-8e6d-171b90d80444") },
                    { new Guid("ae66032b-c718-46a0-96f9-016cd2577d78"), 10m, 3, new Guid("6f5d821a-f421-459b-8e6d-171b90d80444") },
                    { new Guid("bca93801-ffa2-4269-a9f2-6db80c6b689a"), 10m, 4, new Guid("6f5d821a-f421-459b-8e6d-171b90d80444") }
                });

            migrationBuilder.InsertData(
                table: "TeacherSubject",
                columns: new[] { "SubjectId", "UserId" },
                values: new object[] { new Guid("6f5d821a-f421-459b-8e6d-171b90d80444"), new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece") });

            migrationBuilder.InsertData(
                table: "LabWorkStatuses",
                columns: new[] { "Id", "CurrentScore", "IsCompleted", "LabWorkId", "UserId" },
                values: new object[,]
                {
                    { new Guid("29ab33e4-21d3-4a41-aa04-d18701aec85b"), 0m, false, new Guid("98048840-72eb-4f15-8bfe-162904df1d04"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") },
                    { new Guid("5f76e680-d93a-40a8-82ef-8c12d5ca628c"), 0m, false, new Guid("bca93801-ffa2-4269-a9f2-6db80c6b689a"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") },
                    { new Guid("6bece117-22d9-4d8e-8045-6c4f721e83d1"), 0m, false, new Guid("5feed908-153c-4729-bbb5-09ec65543040"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") },
                    { new Guid("adac3f97-a6fe-41f7-a2ed-9293365ea376"), 0m, false, new Guid("ae66032b-c718-46a0-96f9-016cd2577d78"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") }
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
                filter: "\"RoleId\" = '7de8c887-8e3d-42e3-8de2-ec13b0c52f01'");

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
