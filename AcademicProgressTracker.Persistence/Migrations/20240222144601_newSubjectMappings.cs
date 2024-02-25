using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademicProgressTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class newSubjectMappings : Migration
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
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("3380a51b-1f07-4cf0-ace0-c51a7d5a4703"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("4e5c1b99-dbbc-45c1-bdba-9a494b9cb44d"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("59794029-7b72-46a3-b9b0-f678c996ab1f"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("b173fb95-4426-4f37-a591-bc558ccb6f94"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("cba78abd-13a1-475e-b63c-bd9ccc535c3f"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("dfa68c9c-41c1-4414-87c9-fc70ef064737"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("ffac1ca0-c9f0-4bd1-a0f6-62a4e6eaa43b"));

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
                    { new Guid("8b239dfe-4acd-4959-bb9e-7e4adc7ad74a"), 4, null, "ДИПРБ", 2020 },
                    { new Guid("c38a9c37-db2c-44ea-9481-1149352afd45"), 4, null, "ДИИЭБ", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("09935fdc-31a2-4d6b-8333-ca47d8922762"), "Student" },
                    { new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), "Teacher" },
                    { new Guid("f4a9daa5-71d4-4180-88c2-c672b0a48a3f"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "SubjectMappings",
                columns: new[] { "Id", "SubjectNameApiTable", "SubjectNameCurriculum" },
                values: new object[,]
                {
                    { new Guid("00016423-c6ba-438a-b5db-76dfde069e20"), "Сопровождение программного обеспечения", "Сопровождение программного обеспечения" },
                    { new Guid("0b0e8e95-906c-4322-9546-7300f5e62687"), "Теория принятия решений", "Теория принятия решений" },
                    { new Guid("1c57fb10-f83d-431e-99bd-5f363ed524b3"), "Субд postgresql", "СУБД PostgreSQL" },
                    { new Guid("300a6a12-aed4-4b2b-a4e7-f94142ee92e7"), "Управление программными проектами", "Управление программными проектами" },
                    { new Guid("6a9cf853-571c-4a04-a0e3-c7f8be3d0208"), "История россии", "История России" },
                    { new Guid("6f3b42fa-8856-4dcf-a3e8-60f14de6fd6f"), "Разработка приложений asp.net", "Разработка приложений ASP.NET" },
                    { new Guid("83109135-225f-4f38-9137-7e74daa450dc"), "Микропроцессорные системы", "Микропроцессорные системы" },
                    { new Guid("993227ee-2473-433d-ac03-f8084f7ac6ea"), "Самостоятельная работа студента", null },
                    { new Guid("9c1926fb-3ace-4e5f-baf6-73377696becf"), "Проектирование человеко-машинного интерфейса", "Проектирование человеко-машинного интерфейса" },
                    { new Guid("abe4f5df-5fea-43ae-abd2-bc75d5b901e3"), "Элективные дисциплины по физической культуре и спорту", "Элективные дисциплины по физической культуре и спорту" },
                    { new Guid("af150650-8e12-48c7-8ccf-f9007c839294"), "Тестирование программного обеспечения", "Тестирование программного обеспечения" },
                    { new Guid("cd57ef93-098a-4a97-ac0e-67eefa8148f6"), "Разработка и анализ требований , конструирование программного обеспечения", "Разработка и анализ требований, конструирование программного обеспечения" },
                    { new Guid("db2bee0f-d229-4c5b-93cf-2996aef2878d"), "Экономика программной инженерии", "Экономика программной инженерии" },
                    { new Guid("dc869a1d-f55e-403c-be67-0efc15d5fad2"), "Математический аhализ", "Математический анализ" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c"), "teacher@mail.ru", new byte[] { 73, 232, 138, 104, 93, 255, 0, 8, 148, 198, 200, 133, 19, 24, 18, 211, 110, 187, 61, 107, 113, 83, 46, 255, 55, 184, 162, 19, 230, 25, 246, 66, 68, 229, 87, 209, 180, 143, 37, 16, 174, 188, 234, 93, 217, 254, 232, 127, 24, 140, 190, 153, 187, 241, 236, 229, 237, 5, 152, 216, 60, 187, 142, 76 }, new byte[] { 5, 63, 36, 21, 186, 219, 80, 12, 193, 40, 36, 9, 84, 68, 23, 119, 49, 202, 83, 188, 121, 188, 136, 186, 58, 153, 254, 165, 246, 51, 157, 178, 82, 75, 27, 212, 39, 70, 84, 25, 59, 56, 59, 137, 56, 44, 125, 187, 167, 44, 57, 202, 160, 73, 215, 86, 118, 115, 114, 159, 162, 116, 79, 80, 225, 243, 4, 174, 192, 77, 44, 253, 251, 221, 233, 97, 26, 139, 216, 81, 130, 54, 35, 68, 167, 143, 113, 115, 101, 73, 216, 0, 143, 0, 3, 104, 90, 109, 53, 68, 210, 244, 39, 202, 32, 223, 214, 121, 54, 252, 94, 234, 173, 215, 30, 85, 207, 186, 44, 223, 246, 225, 161, 164, 112, 138, 61, 213 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("17947956-c749-4219-9565-c2f3bac4fd82"), "teacherAdmin@mail.ru", new byte[] { 188, 212, 97, 15, 238, 178, 230, 50, 69, 165, 228, 79, 191, 158, 226, 210, 34, 64, 225, 151, 136, 58, 235, 2, 53, 32, 44, 135, 23, 42, 9, 235, 191, 29, 184, 159, 168, 26, 252, 55, 179, 50, 131, 82, 41, 185, 212, 29, 41, 32, 157, 157, 53, 2, 8, 9, 26, 35, 178, 8, 41, 12, 29, 17 }, new byte[] { 15, 128, 176, 243, 237, 197, 72, 102, 72, 160, 84, 5, 116, 28, 13, 124, 6, 94, 114, 5, 190, 253, 72, 126, 219, 99, 163, 82, 254, 43, 13, 161, 85, 230, 241, 166, 81, 136, 135, 114, 10, 193, 20, 52, 17, 237, 228, 93, 228, 179, 183, 28, 232, 222, 134, 180, 169, 156, 76, 53, 70, 62, 2, 94, 150, 218, 1, 230, 34, 141, 107, 160, 65, 92, 122, 97, 220, 4, 56, 190, 116, 74, 98, 155, 195, 210, 67, 195, 88, 88, 218, 128, 227, 251, 223, 193, 102, 29, 208, 83, 4, 194, 177, 231, 231, 185, 195, 198, 147, 128, 250, 166, 121, 165, 64, 192, 55, 154, 121, 82, 70, 59, 66, 173, 3, 208, 85, 46 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("28edf8e3-b727-4928-b963-37fdade17663"), "student@mail.ru", new byte[] { 113, 92, 120, 40, 163, 156, 51, 17, 113, 38, 56, 203, 231, 239, 193, 218, 110, 122, 16, 235, 209, 138, 140, 247, 211, 82, 83, 219, 135, 102, 106, 225, 41, 252, 150, 145, 201, 244, 47, 95, 54, 162, 32, 194, 182, 165, 136, 247, 30, 145, 172, 15, 53, 101, 80, 87, 91, 20, 125, 90, 108, 218, 215, 7 }, new byte[] { 152, 21, 241, 224, 219, 34, 110, 241, 166, 28, 185, 238, 191, 212, 233, 210, 120, 96, 99, 105, 131, 211, 126, 107, 121, 173, 204, 153, 52, 163, 240, 82, 190, 229, 203, 21, 3, 206, 25, 51, 66, 187, 14, 56, 31, 0, 170, 96, 64, 24, 241, 82, 243, 135, 145, 4, 225, 7, 151, 31, 125, 62, 85, 255, 214, 199, 96, 26, 36, 144, 170, 240, 242, 236, 19, 39, 80, 37, 246, 45, 59, 185, 45, 97, 137, 104, 141, 60, 80, 174, 126, 72, 226, 121, 50, 197, 142, 34, 41, 37, 226, 234, 141, 224, 151, 147, 252, 225, 211, 64, 157, 16, 182, 49, 110, 89, 39, 165, 142, 152, 2, 198, 63, 232, 86, 247, 200, 249 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("307af7a3-a320-424b-8911-cce950b3ac92"), "admin@mail.ru", new byte[] { 126, 204, 112, 216, 174, 195, 158, 240, 194, 121, 244, 192, 12, 14, 205, 233, 3, 147, 213, 5, 197, 151, 80, 174, 179, 13, 188, 54, 1, 97, 204, 1, 93, 102, 21, 5, 132, 48, 148, 170, 90, 82, 8, 148, 136, 161, 195, 53, 246, 190, 188, 177, 45, 149, 236, 43, 234, 89, 37, 181, 172, 141, 104, 95 }, new byte[] { 205, 154, 97, 11, 88, 41, 199, 117, 131, 212, 169, 245, 130, 186, 109, 154, 252, 22, 128, 62, 18, 104, 252, 172, 203, 130, 7, 187, 129, 18, 97, 165, 252, 152, 165, 166, 132, 30, 215, 115, 174, 32, 166, 76, 75, 255, 120, 129, 227, 23, 109, 82, 222, 45, 162, 194, 219, 225, 208, 57, 175, 218, 168, 56, 127, 1, 33, 92, 202, 74, 115, 112, 186, 28, 170, 134, 193, 34, 99, 93, 90, 178, 117, 119, 47, 239, 85, 89, 74, 183, 80, 121, 28, 30, 137, 175, 233, 201, 207, 119, 24, 191, 27, 30, 177, 173, 95, 178, 185, 206, 115, 9, 93, 8, 75, 100, 78, 219, 21, 152, 156, 68, 237, 147, 190, 167, 171, 86 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "GroupId", "Name", "Semester" },
                values: new object[] { new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971"), new Guid("8b239dfe-4acd-4959-bb9e-7e4adc7ad74a"), "СУБД PostgreSQL", 0 });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "GroupId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("20a6086e-2af5-48e5-8b78-352ded2ffdf6"), new Guid("8b239dfe-4acd-4959-bb9e-7e4adc7ad74a"), new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c") },
                    { new Guid("83521249-4ee2-45d5-aa5d-8eb8a76faf44"), new Guid("8b239dfe-4acd-4959-bb9e-7e4adc7ad74a"), new Guid("09935fdc-31a2-4d6b-8333-ca47d8922762"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") },
                    { new Guid("f3e80f53-1fda-48e0-b83b-c2ecb3af8eeb"), new Guid("c38a9c37-db2c-44ea-9481-1149352afd45"), new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("09935fdc-31a2-4d6b-8333-ca47d8922762"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") },
                    { new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c") },
                    { new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), new Guid("17947956-c749-4219-9565-c2f3bac4fd82") },
                    { new Guid("f4a9daa5-71d4-4180-88c2-c672b0a48a3f"), new Guid("17947956-c749-4219-9565-c2f3bac4fd82") },
                    { new Guid("f4a9daa5-71d4-4180-88c2-c672b0a48a3f"), new Guid("307af7a3-a320-424b-8911-cce950b3ac92") }
                });

            migrationBuilder.InsertData(
                table: "LabWorks",
                columns: new[] { "Id", "MaximumScore", "Number", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("0cb324f4-3f83-4c04-9196-79ed7eba17c3"), 10m, 3, new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971") },
                    { new Guid("15c2855e-dbb7-4029-8a7c-f12a240ad1ff"), 10m, 4, new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971") },
                    { new Guid("865b5254-9ac3-4df6-85c8-a2274c59bad8"), 10m, 1, new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971") },
                    { new Guid("e63500d2-9280-48d2-bf20-16543d2c78e9"), 10m, 2, new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971") }
                });

            migrationBuilder.InsertData(
                table: "TeacherSubject",
                columns: new[] { "SubjectId", "UserId" },
                values: new object[] { new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971"), new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c") });

            migrationBuilder.InsertData(
                table: "LabWorkStatuses",
                columns: new[] { "Id", "CurrentScore", "IsCompleted", "LabWorkId", "UserId" },
                values: new object[,]
                {
                    { new Guid("001c90a7-d489-4ff5-a238-c73507e57bf3"), 0m, false, new Guid("865b5254-9ac3-4df6-85c8-a2274c59bad8"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") },
                    { new Guid("257ec693-2677-4399-8da9-a8f7c47e7914"), 0m, false, new Guid("e63500d2-9280-48d2-bf20-16543d2c78e9"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") },
                    { new Guid("82ee2645-3496-4931-aecc-72827ca2d821"), 0m, false, new Guid("15c2855e-dbb7-4029-8a7c-f12a240ad1ff"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") },
                    { new Guid("d261b44e-3636-4765-8346-c844543f3424"), 0m, false, new Guid("0cb324f4-3f83-4c04-9196-79ed7eba17c3"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup",
                columns: new[] { "UserId", "RoleId" },
                unique: true,
                filter: "\"RoleId\" = '09935fdc-31a2-4d6b-8333-ca47d8922762'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserGroup_UserId_RoleId",
                table: "UserGroup");

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("001c90a7-d489-4ff5-a238-c73507e57bf3"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("257ec693-2677-4399-8da9-a8f7c47e7914"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("82ee2645-3496-4931-aecc-72827ca2d821"));

            migrationBuilder.DeleteData(
                table: "LabWorkStatuses",
                keyColumn: "Id",
                keyValue: new Guid("d261b44e-3636-4765-8346-c844543f3424"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("00016423-c6ba-438a-b5db-76dfde069e20"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("0b0e8e95-906c-4322-9546-7300f5e62687"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("1c57fb10-f83d-431e-99bd-5f363ed524b3"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("300a6a12-aed4-4b2b-a4e7-f94142ee92e7"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("6a9cf853-571c-4a04-a0e3-c7f8be3d0208"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("6f3b42fa-8856-4dcf-a3e8-60f14de6fd6f"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("83109135-225f-4f38-9137-7e74daa450dc"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("993227ee-2473-433d-ac03-f8084f7ac6ea"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("9c1926fb-3ace-4e5f-baf6-73377696becf"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("abe4f5df-5fea-43ae-abd2-bc75d5b901e3"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("af150650-8e12-48c7-8ccf-f9007c839294"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("cd57ef93-098a-4a97-ac0e-67eefa8148f6"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("db2bee0f-d229-4c5b-93cf-2996aef2878d"));

            migrationBuilder.DeleteData(
                table: "SubjectMappings",
                keyColumn: "Id",
                keyValue: new Guid("dc869a1d-f55e-403c-be67-0efc15d5fad2"));

            migrationBuilder.DeleteData(
                table: "TeacherSubject",
                keyColumns: new[] { "SubjectId", "UserId" },
                keyValues: new object[] { new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971"), new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c") });

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("20a6086e-2af5-48e5-8b78-352ded2ffdf6"));

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("83521249-4ee2-45d5-aa5d-8eb8a76faf44"));

            migrationBuilder.DeleteData(
                table: "UserGroup",
                keyColumn: "Id",
                keyValue: new Guid("f3e80f53-1fda-48e0-b83b-c2ecb3af8eeb"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("09935fdc-31a2-4d6b-8333-ca47d8922762"), new Guid("28edf8e3-b727-4928-b963-37fdade17663") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"), new Guid("17947956-c749-4219-9565-c2f3bac4fd82") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f4a9daa5-71d4-4180-88c2-c672b0a48a3f"), new Guid("17947956-c749-4219-9565-c2f3bac4fd82") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f4a9daa5-71d4-4180-88c2-c672b0a48a3f"), new Guid("307af7a3-a320-424b-8911-cce950b3ac92") });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("c38a9c37-db2c-44ea-9481-1149352afd45"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("0cb324f4-3f83-4c04-9196-79ed7eba17c3"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("15c2855e-dbb7-4029-8a7c-f12a240ad1ff"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("865b5254-9ac3-4df6-85c8-a2274c59bad8"));

            migrationBuilder.DeleteData(
                table: "LabWorks",
                keyColumn: "Id",
                keyValue: new Guid("e63500d2-9280-48d2-bf20-16543d2c78e9"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("09935fdc-31a2-4d6b-8333-ca47d8922762"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ccd97ef2-a778-4c63-831b-5c3616448ae5"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f4a9daa5-71d4-4180-88c2-c672b0a48a3f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("03190e00-b8c9-40ee-a919-2dd75273c91c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("17947956-c749-4219-9565-c2f3bac4fd82"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28edf8e3-b727-4928-b963-37fdade17663"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("307af7a3-a320-424b-8911-cce950b3ac92"));

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: new Guid("7d03f28f-b8c2-4af9-9cd8-1e24768ce971"));

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: new Guid("8b239dfe-4acd-4959-bb9e-7e4adc7ad74a"));

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
    }
}
