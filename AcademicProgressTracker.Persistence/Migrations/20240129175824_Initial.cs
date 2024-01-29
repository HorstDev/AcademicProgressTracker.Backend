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
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("263e4c27-f70e-4922-b5b8-3af54bedb27e"), "Admin" },
                    { new Guid("64685a36-4404-4283-a2f1-119b5a82cdca"), "Student" },
                    { new Guid("e37aa3aa-b51b-422d-9b5a-e3316032de05"), "Teacher" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("227bcf31-420a-4d42-a262-481796e1a762"), "teacher@mail.ru", new byte[] { 222, 41, 231, 160, 39, 231, 198, 86, 105, 168, 42, 54, 23, 144, 105, 143, 118, 153, 248, 221, 174, 132, 188, 205, 141, 173, 152, 142, 108, 254, 142, 248, 151, 133, 238, 66, 29, 187, 241, 144, 246, 64, 220, 144, 58, 252, 62, 134, 75, 47, 116, 254, 49, 47, 196, 186, 51, 17, 231, 0, 187, 74, 11, 7 }, new byte[] { 97, 16, 19, 254, 176, 153, 223, 46, 31, 238, 29, 0, 2, 3, 141, 241, 46, 100, 195, 59, 109, 160, 172, 51, 16, 246, 12, 198, 169, 92, 239, 135, 241, 213, 124, 214, 134, 15, 13, 22, 20, 167, 105, 15, 27, 11, 14, 193, 145, 139, 93, 81, 208, 90, 129, 60, 29, 219, 201, 153, 239, 243, 118, 104, 96, 224, 31, 3, 184, 238, 78, 190, 254, 134, 150, 124, 193, 225, 185, 215, 161, 139, 46, 153, 111, 73, 40, 153, 52, 69, 197, 96, 45, 12, 135, 16, 76, 98, 31, 238, 238, 158, 135, 7, 125, 28, 62, 50, 113, 119, 124, 1, 220, 44, 201, 171, 191, 247, 96, 73, 199, 83, 246, 185, 229, 83, 135, 17 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("2a1841da-e82f-4b5c-8089-51729f7a4202"), "admin@mail.ru", new byte[] { 148, 125, 215, 16, 217, 204, 150, 229, 38, 34, 184, 200, 15, 39, 15, 96, 204, 240, 93, 108, 232, 89, 133, 186, 54, 154, 221, 162, 228, 165, 218, 1, 194, 181, 100, 143, 171, 244, 122, 4, 31, 28, 208, 68, 132, 197, 217, 57, 246, 90, 11, 66, 48, 173, 175, 152, 218, 70, 145, 143, 65, 165, 163, 139 }, new byte[] { 90, 28, 239, 213, 229, 249, 11, 28, 235, 240, 45, 142, 29, 144, 144, 32, 138, 121, 107, 46, 206, 14, 82, 105, 231, 246, 128, 244, 203, 222, 170, 131, 179, 111, 243, 171, 212, 6, 132, 16, 179, 27, 239, 180, 6, 56, 109, 118, 117, 224, 7, 212, 23, 242, 243, 5, 112, 24, 151, 61, 97, 253, 46, 49, 24, 209, 62, 132, 76, 187, 155, 47, 231, 4, 254, 226, 113, 26, 200, 149, 232, 92, 150, 153, 0, 177, 177, 96, 138, 138, 192, 70, 110, 217, 6, 190, 245, 244, 16, 29, 62, 71, 28, 29, 98, 126, 178, 56, 81, 113, 40, 170, 103, 221, 54, 182, 56, 167, 101, 165, 195, 238, 233, 238, 123, 189, 142, 0 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("600fb485-1400-426c-9312-350616ca09d3"), "student@mail.ru", new byte[] { 73, 174, 219, 225, 213, 183, 44, 81, 43, 251, 110, 171, 9, 69, 43, 138, 208, 91, 13, 127, 111, 228, 80, 4, 51, 251, 145, 253, 154, 200, 6, 111, 215, 133, 46, 10, 194, 73, 126, 135, 171, 97, 236, 42, 234, 168, 139, 118, 225, 46, 113, 35, 137, 36, 160, 136, 107, 205, 30, 232, 219, 60, 27, 140 }, new byte[] { 161, 14, 186, 30, 39, 194, 144, 193, 103, 232, 67, 201, 201, 21, 166, 137, 234, 214, 94, 224, 30, 239, 163, 43, 91, 138, 203, 112, 158, 35, 17, 146, 74, 133, 202, 85, 73, 118, 183, 233, 169, 156, 85, 3, 176, 238, 235, 179, 63, 218, 196, 70, 52, 9, 178, 206, 173, 35, 245, 248, 185, 218, 5, 92, 193, 59, 231, 243, 216, 77, 59, 125, 133, 211, 37, 70, 71, 194, 0, 139, 29, 32, 162, 144, 21, 69, 94, 17, 82, 135, 224, 124, 57, 175, 233, 106, 255, 122, 74, 202, 185, 80, 128, 25, 237, 143, 6, 200, 201, 115, 110, 65, 133, 86, 210, 79, 57, 119, 90, 32, 92, 128, 113, 33, 253, 132, 212, 7 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("9905bb0b-5703-4dff-80df-5609afcc70ed"), "teacherAdmin@mail.ru", new byte[] { 253, 51, 89, 9, 244, 193, 213, 231, 155, 27, 25, 163, 99, 170, 122, 70, 19, 78, 105, 13, 232, 83, 226, 124, 56, 151, 44, 34, 7, 20, 28, 127, 17, 122, 31, 151, 29, 163, 5, 47, 75, 133, 191, 223, 117, 219, 254, 6, 163, 73, 126, 122, 55, 75, 100, 21, 16, 139, 201, 73, 48, 77, 167, 197 }, new byte[] { 90, 64, 169, 95, 129, 115, 74, 29, 111, 136, 151, 128, 242, 227, 242, 152, 91, 217, 206, 36, 242, 6, 229, 34, 95, 141, 161, 204, 114, 48, 106, 203, 21, 138, 62, 47, 143, 140, 111, 110, 41, 83, 41, 121, 14, 129, 9, 164, 224, 216, 73, 222, 28, 8, 210, 25, 109, 84, 213, 37, 61, 33, 139, 60, 237, 251, 45, 162, 64, 31, 129, 236, 33, 169, 141, 197, 96, 247, 129, 105, 16, 193, 83, 236, 165, 42, 105, 235, 10, 93, 158, 63, 11, 87, 21, 86, 143, 43, 99, 211, 172, 255, 217, 110, 193, 169, 244, 180, 112, 103, 166, 134, 49, 140, 19, 43, 92, 141, 49, 255, 46, 95, 115, 231, 69, 155, 114, 203 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("263e4c27-f70e-4922-b5b8-3af54bedb27e"), new Guid("2a1841da-e82f-4b5c-8089-51729f7a4202") },
                    { new Guid("263e4c27-f70e-4922-b5b8-3af54bedb27e"), new Guid("9905bb0b-5703-4dff-80df-5609afcc70ed") },
                    { new Guid("64685a36-4404-4283-a2f1-119b5a82cdca"), new Guid("600fb485-1400-426c-9312-350616ca09d3") },
                    { new Guid("e37aa3aa-b51b-422d-9b5a-e3316032de05"), new Guid("227bcf31-420a-4d42-a262-481796e1a762") },
                    { new Guid("e37aa3aa-b51b-422d-9b5a-e3316032de05"), new Guid("9905bb0b-5703-4dff-80df-5609afcc70ed") }
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
                filter: "\"RoleId\" = '64685a36-4404-4283-a2f1-119b5a82cdca'");

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
