using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademicProgressTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Mappingsforsubjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup");

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("29ab33e4-21d3-4a41-aa04-d18701aec85b"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("5f76e680-d93a-40a8-82ef-8c12d5ca628c"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("6bece117-22d9-4d8e-8045-6c4f721e83d1"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("adac3f97-a6fe-41f7-a2ed-9293365ea376"));

            migrationBuilder.DeleteData(
                table: "TeacherSubject",
                keyColumns: new[] { "SubjectId", "UserId" },
                keyValues: new object[] { new Guid("6f5d821a-f421-459b-8e6d-171b90d80444"), new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece") });

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("573cf133-8c57-410f-b5dd-3dcf2d359d4e"));

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("b52c035c-495a-4f36-a1bf-61b06b055977"));

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("e30817d5-4173-46aa-855d-fa267e574aee"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), new Guid("24b3720e-a08f-4568-9498-237ceecc41f0") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"), new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("56ec748e-6719-4534-8491-b332582b95ad"), new Guid("1b73e408-2fe1-455c-b9e5-2a3dc81481aa") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("56ec748e-6719-4534-8491-b332582b95ad"), new Guid("24b3720e-a08f-4568-9498-237ceecc41f0") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7de8c887-8e3d-42e3-8de2-ec13b0c52f01"), new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8") });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("c1afa60f-c6aa-46b0-b97e-5011c02d3ba4"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("5feed908-153c-4729-bbb5-09ec65543040"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("98048840-72eb-4f15-8bfe-162904df1d04"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("ae66032b-c718-46a0-96f9-016cd2577d78"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("bca93801-ffa2-4269-a9f2-6db80c6b689a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00542190-6d74-4ca5-98be-a9c9ced21c3c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("56ec748e-6719-4534-8491-b332582b95ad"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7de8c887-8e3d-42e3-8de2-ec13b0c52f01"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b73e408-2fe1-455c-b9e5-2a3dc81481aa"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("24b3720e-a08f-4568-9498-237ceecc41f0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5a2e67ee-7554-4993-b248-1c8ce83d72d8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6699c2d3-7fb4-4b13-b337-fa9e2492bece"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("6f5d821a-f421-459b-8e6d-171b90d80444"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("b44185a6-d896-4085-a48e-daaee4b9d3ec"));

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

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Course", "CurriculumExcelDocument", "Name", "YearCreated" },
                values: new object[,]
                {
                    { new Guid("0bbeed8f-6918-40c8-8910-809914dd61d2"), 4, null, "ДИИЭБ", 2020 },
                    { new Guid("dbc8048f-26ed-4212-bc46-5ba3a438a0d3"), 4, null, "ДИПРБ", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("67ec963b-940a-48c0-9299-940f6738633c"), "Student" },
                    { new Guid("7fe77e53-3bcb-4df9-92ca-485784d828eb"), "Admin" },
                    { new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), "Teacher" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("3380a51b-1f07-4cf0-ace0-c51a7d5a4703"), "История россии", "История России" },
                    { new Guid("4e5c1b99-dbbc-45c1-bdba-9a494b9cb44d"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("59794029-7b72-46a3-b9b0-f678c996ab1f"), "Математический аhализ", "Математический анализ" },
                    { new Guid("b173fb95-4426-4f37-a591-bc558ccb6f94"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("cba78abd-13a1-475e-b63c-bd9ccc535c3f"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("dfa68c9c-41c1-4414-87c9-fc70ef064737"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("ffac1ca0-c9f0-4bd1-a0f6-62a4e6eaa43b"), "Самостоятельная работа студента", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a"), "student@mail.ru", new byte[] { 41, 180, 22, 114, 117, 43, 95, 250, 200, 196, 136, 74, 36, 193, 10, 26, 158, 165, 104, 76, 151, 122, 201, 195, 125, 26, 211, 38, 76, 219, 87, 145, 73, 68, 49, 37, 170, 22, 240, 127, 228, 216, 224, 223, 148, 128, 99, 101, 0, 134, 228, 69, 207, 144, 61, 61, 51, 115, 115, 254, 153, 172, 246, 205 }, new byte[] { 86, 223, 221, 148, 125, 156, 173, 160, 233, 69, 111, 110, 230, 62, 146, 172, 227, 38, 181, 59, 129, 85, 236, 40, 150, 107, 173, 231, 242, 246, 36, 12, 66, 135, 23, 153, 216, 6, 150, 150, 121, 169, 220, 234, 36, 182, 53, 159, 29, 208, 64, 164, 189, 41, 247, 108, 49, 96, 211, 121, 97, 178, 31, 242, 85, 67, 78, 73, 176, 128, 142, 130, 108, 182, 199, 173, 17, 105, 87, 76, 126, 37, 216, 169, 230, 1, 240, 211, 107, 161, 139, 23, 233, 136, 141, 30, 88, 162, 103, 67, 135, 250, 178, 245, 204, 4, 141, 199, 19, 212, 157, 34, 249, 244, 65, 213, 86, 131, 156, 240, 19, 204, 184, 215, 13, 231, 104, 30 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("5db3e422-1cae-47a5-a705-7f95dc12366a"), "admin@mail.ru", new byte[] { 65, 144, 58, 107, 255, 241, 128, 7, 235, 127, 248, 127, 239, 212, 15, 115, 236, 225, 78, 120, 89, 28, 120, 205, 75, 116, 36, 183, 80, 145, 68, 87, 90, 89, 173, 92, 73, 137, 131, 228, 119, 63, 2, 69, 147, 251, 227, 23, 0, 130, 184, 193, 255, 77, 7, 107, 15, 67, 199, 193, 183, 176, 199, 135 }, new byte[] { 32, 61, 91, 251, 209, 76, 30, 78, 72, 213, 159, 191, 171, 47, 237, 103, 193, 198, 26, 228, 152, 195, 101, 167, 73, 33, 116, 126, 102, 193, 244, 186, 14, 185, 104, 99, 217, 39, 195, 29, 212, 227, 217, 4, 39, 13, 90, 210, 167, 0, 79, 179, 104, 127, 82, 32, 216, 77, 140, 171, 152, 119, 136, 228, 50, 103, 20, 180, 155, 227, 45, 17, 213, 4, 236, 69, 248, 120, 55, 165, 77, 32, 155, 8, 231, 19, 20, 39, 91, 157, 47, 172, 48, 127, 87, 49, 94, 47, 242, 214, 122, 76, 2, 21, 26, 235, 53, 59, 216, 222, 20, 49, 164, 252, 141, 54, 182, 208, 231, 245, 93, 6, 61, 226, 205, 161, 29, 26 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("72ddebb1-fafa-47e4-abdd-f851b37c86cd"), "teacherAdmin@mail.ru", new byte[] { 21, 176, 133, 198, 96, 169, 127, 54, 192, 152, 233, 208, 197, 49, 151, 67, 244, 124, 194, 246, 193, 185, 81, 212, 66, 107, 87, 213, 42, 61, 149, 33, 6, 165, 254, 163, 82, 91, 62, 54, 226, 191, 24, 148, 140, 128, 150, 202, 190, 166, 140, 219, 130, 93, 121, 115, 73, 128, 87, 15, 133, 56, 161, 188 }, new byte[] { 5, 100, 166, 100, 234, 146, 156, 255, 120, 13, 65, 230, 107, 64, 4, 185, 163, 12, 193, 40, 54, 101, 16, 88, 106, 173, 99, 215, 223, 159, 136, 180, 73, 222, 62, 162, 142, 89, 8, 57, 144, 212, 143, 255, 164, 205, 109, 55, 222, 7, 134, 135, 61, 66, 15, 1, 111, 18, 129, 110, 165, 200, 124, 33, 10, 28, 86, 98, 155, 208, 118, 3, 199, 155, 186, 248, 245, 107, 157, 4, 179, 255, 133, 5, 109, 53, 14, 65, 102, 230, 109, 160, 212, 98, 250, 155, 204, 235, 90, 208, 190, 58, 93, 191, 209, 186, 115, 162, 69, 119, 104, 167, 172, 230, 34, 122, 116, 246, 198, 102, 221, 144, 190, 169, 7, 192, 242, 47 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11"), "teacher@mail.ru", new byte[] { 152, 132, 191, 178, 97, 160, 48, 26, 100, 180, 26, 124, 17, 10, 199, 96, 2, 169, 182, 159, 66, 91, 8, 45, 241, 31, 8, 83, 39, 164, 5, 203, 64, 140, 116, 207, 82, 230, 82, 99, 73, 153, 123, 34, 122, 214, 20, 95, 237, 180, 111, 193, 91, 33, 174, 163, 231, 201, 13, 164, 201, 225, 201, 156 }, new byte[] { 255, 237, 90, 146, 155, 125, 36, 139, 86, 70, 86, 174, 13, 64, 254, 209, 6, 160, 146, 177, 143, 253, 161, 153, 136, 167, 151, 172, 83, 139, 201, 71, 48, 210, 249, 71, 151, 99, 89, 128, 40, 27, 245, 120, 168, 2, 236, 62, 137, 219, 138, 70, 113, 4, 174, 48, 204, 173, 15, 110, 130, 184, 135, 141, 126, 63, 37, 229, 183, 225, 75, 156, 117, 53, 159, 67, 117, 82, 51, 34, 47, 167, 168, 245, 215, 5, 87, 65, 71, 234, 73, 163, 54, 112, 43, 194, 120, 67, 114, 191, 92, 6, 202, 165, 70, 158, 103, 125, 58, 108, 180, 47, 223, 38, 151, 115, 0, 78, 12, 4, 47, 217, 105, 154, 70, 8, 249, 174 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "GroupId", "Name", "Semester" },
                values: new object[] { new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1"), new Guid("dbc8048f-26ed-4212-bc46-5ba3a438a0d3"), "СУБД PostgreSQL", 0 });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "GroupId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("0069135b-d957-44cf-8a0a-86800352ac00"), new Guid("0bbeed8f-6918-40c8-8910-809914dd61d2"), new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11") },
                    { new Guid("c9c0ca1d-a83f-4299-a518-e2bf6cd6f439"), new Guid("dbc8048f-26ed-4212-bc46-5ba3a438a0d3"), new Guid("67ec963b-940a-48c0-9299-940f6738633c"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") },
                    { new Guid("f075b99f-acf3-45b5-b7a3-1fcadb5ad790"), new Guid("dbc8048f-26ed-4212-bc46-5ba3a438a0d3"), new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("67ec963b-940a-48c0-9299-940f6738633c"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") },
                    { new Guid("7fe77e53-3bcb-4df9-92ca-485784d828eb"), new Guid("5db3e422-1cae-47a5-a705-7f95dc12366a") },
                    { new Guid("7fe77e53-3bcb-4df9-92ca-485784d828eb"), new Guid("72ddebb1-fafa-47e4-abdd-f851b37c86cd") },
                    { new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), new Guid("72ddebb1-fafa-47e4-abdd-f851b37c86cd") },
                    { new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11") }
                });

            migrationBuilder.InsertData(
                table: "LabWorks",
                columns: new[] { "Id", "MaximumScore", "Number", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("151c61f7-2264-4433-b4d9-ac66883c5423"), 10m, 1, new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1") },
                    { new Guid("3ec4bf0d-0fc4-4e6c-9832-387793dd327b"), 10m, 2, new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1") },
                    { new Guid("7a511da6-9b1b-4606-bc14-6a2689933298"), 10m, 3, new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1") },
                    { new Guid("921c8603-1467-448c-97a5-ca318241761a"), 10m, 4, new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1") }
                });

            migrationBuilder.InsertData(
                table: "TeacherSubject",
                columns: new[] { "SubjectId", "UserId" },
                values: new object[] { new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1"), new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11") });

            migrationBuilder.InsertData(
                table: "LabWorkStatuses",
                columns: new[] { "Id", "CurrentScore", "IsCompleted", "LabWorkId", "UserId" },
                values: new object[,]
                {
                    { new Guid("00eec71d-6916-456c-ada8-a0455e6bdffd"), 0m, false, new Guid("151c61f7-2264-4433-b4d9-ac66883c5423"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") },
                    { new Guid("07af81f0-0b6c-4548-9c92-9d49a3c55f90"), 0m, false, new Guid("3ec4bf0d-0fc4-4e6c-9832-387793dd327b"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") },
                    { new Guid("4f8d787c-2a04-4df5-8d1a-47374f2b121a"), 0m, false, new Guid("7a511da6-9b1b-4606-bc14-6a2689933298"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") },
                    { new Guid("df94c98d-faea-452c-accb-afc9f4cc6e2d"), 0m, false, new Guid("921c8603-1467-448c-97a5-ca318241761a"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"RoleId\" = '67ec963b-940a-48c0-9299-940f6738633c'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectMappings");

            migrationBuilder.DropIndex(
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup");

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("00eec71d-6916-456c-ada8-a0455e6bdffd"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("07af81f0-0b6c-4548-9c92-9d49a3c55f90"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("4f8d787c-2a04-4df5-8d1a-47374f2b121a"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("df94c98d-faea-452c-accb-afc9f4cc6e2d"));

            migrationBuilder.DeleteData(
                table: "TeacherSubject",
                keyColumns: new[] { "SubjectId", "UserId" },
                keyValues: new object[] { new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1"), new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11") });

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("0069135b-d957-44cf-8a0a-86800352ac00"));

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("c9c0ca1d-a83f-4299-a518-e2bf6cd6f439"));

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("f075b99f-acf3-45b5-b7a3-1fcadb5ad790"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("67ec963b-940a-48c0-9299-940f6738633c"), new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7fe77e53-3bcb-4df9-92ca-485784d828eb"), new Guid("5db3e422-1cae-47a5-a705-7f95dc12366a") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("7fe77e53-3bcb-4df9-92ca-485784d828eb"), new Guid("72ddebb1-fafa-47e4-abdd-f851b37c86cd") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), new Guid("72ddebb1-fafa-47e4-abdd-f851b37c86cd") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"), new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11") });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("0bbeed8f-6918-40c8-8910-809914dd61d2"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("151c61f7-2264-4433-b4d9-ac66883c5423"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("3ec4bf0d-0fc4-4e6c-9832-387793dd327b"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("7a511da6-9b1b-4606-bc14-6a2689933298"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("921c8603-1467-448c-97a5-ca318241761a"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("67ec963b-940a-48c0-9299-940f6738633c"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7fe77e53-3bcb-4df9-92ca-485784d828eb"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f1a77887-ff0c-4b04-b19c-fa1e05746d96"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5c142cc6-cbed-4068-b7d1-cbe3c37d3a2a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5db3e422-1cae-47a5-a705-7f95dc12366a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("72ddebb1-fafa-47e4-abdd-f851b37c86cd"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("95e3df5d-ce44-4f29-95b0-fe3b5b6f2e11"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("c89451dc-37c6-45e7-8ff2-3c6e7721ace1"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("dbc8048f-26ed-4212-bc46-5ba3a438a0d3"));

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
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"RoleId\" = '7de8c887-8e3d-42e3-8de2-ec13b0c52f01'");
        }
    }
}
