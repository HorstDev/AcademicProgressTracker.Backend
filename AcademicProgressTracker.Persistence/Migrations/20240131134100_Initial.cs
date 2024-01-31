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
                    MaximumScore = table.Column<float>(type: "real", nullable: false),
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
                name: "LabWorkStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
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
                    { new Guid("62264dfc-8eab-43f2-bffd-a4f165b4dace"), 4, "ДИПРБ", 2020 },
                    { new Guid("8e5a260d-1a98-4d0c-9683-75a418b1b560"), 4, "ДИИЭБ", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"), "Teacher" },
                    { new Guid("3814c6e4-5cca-4681-8bd5-998aef007642"), "Student" },
                    { new Guid("472e417a-2f85-4dc4-8028-da121e1746eb"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("01179748-f8ab-4e8e-9406-953a16a28486"), "teacher@mail.ru", new byte[] { 137, 42, 9, 21, 129, 3, 154, 50, 197, 212, 59, 13, 182, 151, 202, 39, 39, 99, 237, 89, 151, 38, 24, 124, 247, 176, 169, 55, 103, 191, 136, 157, 93, 108, 252, 127, 113, 85, 40, 33, 174, 194, 183, 199, 32, 115, 186, 239, 176, 202, 0, 85, 204, 218, 91, 95, 168, 188, 142, 141, 119, 34, 69, 229 }, new byte[] { 93, 227, 166, 52, 14, 47, 106, 18, 95, 145, 191, 189, 114, 184, 162, 157, 167, 81, 135, 213, 35, 226, 105, 81, 29, 55, 146, 190, 171, 146, 254, 161, 5, 3, 243, 57, 177, 126, 77, 194, 82, 254, 222, 118, 77, 24, 44, 46, 163, 196, 87, 213, 119, 236, 75, 192, 52, 81, 158, 231, 75, 174, 9, 15, 49, 84, 251, 34, 104, 198, 246, 88, 132, 252, 247, 178, 66, 140, 222, 33, 214, 78, 103, 248, 14, 219, 37, 196, 167, 65, 146, 8, 53, 222, 98, 146, 184, 160, 146, 248, 10, 45, 120, 22, 165, 73, 249, 58, 155, 237, 133, 241, 33, 5, 134, 175, 124, 150, 57, 145, 96, 21, 156, 104, 217, 211, 210, 116 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("1529e062-f8d8-4d2f-aec8-e348b64080e0"), "admin@mail.ru", new byte[] { 151, 94, 212, 30, 156, 124, 74, 139, 34, 32, 189, 202, 11, 183, 225, 24, 8, 113, 196, 192, 12, 62, 11, 118, 148, 54, 70, 46, 82, 197, 191, 188, 37, 102, 234, 28, 202, 244, 206, 18, 255, 68, 228, 123, 106, 247, 114, 125, 37, 77, 102, 40, 211, 69, 130, 171, 231, 80, 243, 244, 30, 5, 180, 33 }, new byte[] { 112, 221, 30, 81, 248, 138, 186, 222, 44, 229, 168, 82, 183, 117, 224, 36, 238, 92, 173, 195, 225, 239, 131, 34, 66, 34, 45, 120, 168, 110, 176, 53, 181, 207, 157, 216, 127, 1, 130, 115, 0, 186, 93, 154, 50, 3, 33, 32, 67, 128, 159, 217, 229, 129, 103, 91, 233, 158, 220, 254, 232, 149, 150, 236, 70, 27, 175, 29, 128, 21, 195, 48, 46, 116, 39, 219, 120, 21, 182, 110, 191, 198, 237, 92, 237, 199, 40, 161, 195, 15, 128, 26, 84, 231, 214, 87, 210, 76, 133, 168, 100, 250, 91, 123, 188, 245, 227, 29, 161, 168, 174, 87, 59, 13, 132, 201, 80, 45, 136, 45, 245, 82, 83, 163, 69, 45, 248, 30 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("4da62e83-b06d-4a96-af7a-a4fb9f4a6987"), "teacherAdmin@mail.ru", new byte[] { 34, 32, 34, 218, 153, 44, 191, 216, 245, 164, 177, 106, 91, 118, 172, 13, 84, 67, 4, 116, 159, 69, 235, 32, 164, 154, 220, 16, 187, 51, 63, 188, 22, 99, 199, 70, 133, 103, 205, 17, 150, 138, 246, 126, 116, 201, 164, 16, 30, 112, 93, 42, 86, 155, 17, 26, 75, 95, 93, 220, 71, 170, 99, 29 }, new byte[] { 189, 12, 107, 62, 200, 77, 85, 224, 61, 172, 231, 158, 238, 236, 118, 153, 226, 124, 18, 237, 49, 161, 43, 128, 243, 234, 45, 163, 65, 38, 183, 133, 207, 31, 9, 246, 242, 183, 202, 6, 34, 128, 96, 68, 20, 52, 220, 153, 28, 226, 221, 61, 161, 91, 14, 120, 134, 98, 140, 248, 177, 120, 86, 80, 246, 160, 20, 140, 169, 240, 218, 87, 229, 37, 2, 43, 51, 26, 60, 128, 61, 164, 170, 192, 155, 217, 109, 47, 89, 148, 114, 153, 13, 45, 240, 64, 42, 163, 250, 250, 101, 104, 161, 250, 44, 22, 70, 177, 254, 16, 212, 189, 163, 148, 255, 140, 160, 240, 44, 202, 220, 186, 161, 4, 147, 38, 248, 109 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("7a729276-17cc-4f95-94c1-a4c5315b203f"), "student@mail.ru", new byte[] { 250, 187, 49, 188, 160, 77, 112, 103, 44, 48, 183, 48, 202, 65, 73, 119, 37, 172, 188, 107, 7, 230, 120, 34, 116, 12, 234, 28, 33, 227, 25, 197, 34, 89, 124, 98, 16, 81, 41, 2, 97, 242, 88, 215, 160, 75, 149, 27, 236, 136, 235, 76, 120, 156, 241, 98, 74, 102, 72, 10, 236, 164, 223, 146 }, new byte[] { 5, 3, 170, 62, 125, 28, 167, 82, 94, 222, 10, 230, 140, 248, 68, 28, 28, 109, 105, 32, 112, 118, 232, 183, 152, 6, 204, 226, 255, 213, 46, 155, 13, 218, 15, 252, 30, 214, 144, 44, 239, 159, 3, 137, 170, 28, 138, 134, 158, 93, 205, 236, 152, 246, 124, 70, 238, 196, 31, 127, 51, 116, 40, 235, 10, 228, 167, 250, 100, 251, 14, 185, 235, 8, 139, 92, 85, 166, 100, 242, 192, 146, 113, 75, 48, 184, 169, 219, 93, 1, 250, 115, 106, 228, 44, 216, 183, 62, 16, 158, 189, 111, 224, 131, 120, 140, 74, 101, 220, 142, 150, 65, 183, 179, 201, 176, 26, 209, 142, 50, 16, 229, 9, 171, 114, 59, 188, 221 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "GroupId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("14d30da8-386d-49f1-bc51-6ddccafe541d"), new Guid("62264dfc-8eab-43f2-bffd-a4f165b4dace"), new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"), new Guid("01179748-f8ab-4e8e-9406-953a16a28486") },
                    { new Guid("53b3f9a7-1223-4055-9970-5837667b2704"), new Guid("8e5a260d-1a98-4d0c-9683-75a418b1b560"), new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"), new Guid("01179748-f8ab-4e8e-9406-953a16a28486") },
                    { new Guid("56abbbde-3e74-41be-93e5-e81fdf815129"), new Guid("62264dfc-8eab-43f2-bffd-a4f165b4dace"), new Guid("3814c6e4-5cca-4681-8bd5-998aef007642"), new Guid("7a729276-17cc-4f95-94c1-a4c5315b203f") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"), new Guid("01179748-f8ab-4e8e-9406-953a16a28486") },
                    { new Guid("2117f306-e39e-4544-b522-217d8b5b8a1c"), new Guid("4da62e83-b06d-4a96-af7a-a4fb9f4a6987") },
                    { new Guid("3814c6e4-5cca-4681-8bd5-998aef007642"), new Guid("7a729276-17cc-4f95-94c1-a4c5315b203f") },
                    { new Guid("472e417a-2f85-4dc4-8028-da121e1746eb"), new Guid("1529e062-f8d8-4d2f-aec8-e348b64080e0") },
                    { new Guid("472e417a-2f85-4dc4-8028-da121e1746eb"), new Guid("4da62e83-b06d-4a96-af7a-a4fb9f4a6987") }
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
                filter: "\"RoleId\" = '3814c6e4-5cca-4681-8bd5-998aef007642'");

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
