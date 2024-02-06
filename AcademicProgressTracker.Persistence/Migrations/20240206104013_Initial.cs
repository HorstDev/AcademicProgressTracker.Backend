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
                    { new Guid("03829da0-798c-4d92-a81c-6c854688df1e"), 4, "ДИПРБ", 2020 },
                    { new Guid("3b91b3b9-fafe-4986-a7f3-c1e09025e569"), 4, "ДИИЭБ", 2020 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03f4c303-c131-4233-8297-a040fb67c220"), "Teacher" },
                    { new Guid("6cb0202b-2e70-4475-b574-4eca31f5c440"), "Student" },
                    { new Guid("eb040d7a-ad98-4ab4-ab25-3d3e4edb46f0"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "PasswordSalt", "RefreshToken", "TokenCreated", "TokenExpires" },
                values: new object[,]
                {
                    { new Guid("1970342a-0249-45be-b3ea-901fd76cfeea"), "teacher@mail.ru", new byte[] { 79, 27, 230, 129, 186, 237, 67, 167, 95, 208, 185, 213, 152, 16, 102, 90, 135, 7, 204, 30, 111, 20, 187, 96, 49, 235, 32, 169, 35, 165, 26, 225, 148, 64, 11, 84, 188, 48, 231, 3, 86, 4, 18, 94, 85, 58, 186, 241, 208, 229, 42, 119, 242, 55, 75, 208, 98, 208, 132, 72, 143, 176, 205, 39 }, new byte[] { 121, 12, 244, 245, 198, 192, 204, 213, 158, 230, 227, 138, 67, 21, 26, 243, 209, 136, 218, 72, 39, 61, 139, 31, 15, 178, 48, 93, 25, 114, 55, 85, 165, 190, 94, 108, 216, 4, 32, 52, 85, 122, 50, 7, 32, 240, 170, 198, 206, 214, 13, 76, 198, 127, 223, 149, 203, 229, 53, 107, 234, 8, 197, 93, 198, 40, 58, 190, 73, 109, 227, 106, 223, 150, 108, 135, 240, 137, 185, 72, 2, 147, 18, 136, 17, 152, 154, 210, 68, 160, 25, 102, 238, 177, 83, 124, 160, 111, 100, 49, 226, 241, 38, 29, 92, 202, 73, 8, 53, 188, 17, 55, 198, 172, 255, 208, 211, 171, 113, 191, 177, 241, 251, 112, 39, 252, 209, 181 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("27c926f1-a755-484c-acd5-d4b9f573da6b"), "admin@mail.ru", new byte[] { 17, 236, 9, 208, 193, 27, 220, 20, 110, 104, 18, 56, 99, 249, 248, 116, 171, 149, 29, 176, 38, 27, 172, 201, 219, 208, 102, 247, 160, 49, 160, 51, 129, 84, 131, 9, 85, 98, 13, 192, 73, 232, 56, 52, 141, 231, 235, 246, 88, 89, 176, 233, 107, 202, 12, 195, 111, 63, 78, 174, 36, 83, 44, 53 }, new byte[] { 88, 213, 49, 72, 245, 119, 99, 119, 227, 5, 1, 114, 78, 43, 140, 191, 244, 208, 104, 21, 133, 165, 34, 44, 91, 58, 195, 215, 30, 153, 199, 69, 179, 215, 231, 254, 216, 173, 9, 174, 236, 196, 146, 80, 205, 71, 154, 154, 76, 165, 83, 138, 5, 182, 43, 138, 65, 136, 165, 201, 255, 192, 127, 58, 225, 106, 90, 188, 35, 135, 116, 112, 251, 120, 62, 23, 51, 189, 60, 7, 15, 69, 125, 107, 128, 91, 115, 115, 138, 126, 56, 71, 199, 24, 103, 77, 232, 116, 43, 215, 180, 51, 147, 47, 36, 12, 26, 94, 105, 222, 67, 105, 247, 39, 203, 228, 73, 56, 39, 231, 16, 212, 149, 107, 38, 144, 64, 254 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("761c31e6-202f-420f-8ec3-0bb178544caa"), "teacherAdmin@mail.ru", new byte[] { 85, 97, 113, 99, 253, 6, 70, 174, 190, 38, 24, 84, 183, 180, 237, 12, 237, 122, 8, 141, 245, 35, 87, 223, 110, 123, 81, 230, 55, 111, 177, 175, 237, 179, 128, 86, 106, 131, 172, 168, 23, 249, 71, 46, 204, 50, 41, 144, 243, 85, 119, 248, 18, 137, 18, 161, 75, 111, 200, 189, 255, 151, 55, 12 }, new byte[] { 219, 184, 145, 56, 70, 155, 236, 161, 249, 12, 52, 204, 130, 226, 138, 128, 99, 130, 44, 217, 136, 19, 57, 97, 215, 251, 55, 251, 59, 61, 57, 10, 60, 121, 38, 152, 202, 215, 77, 114, 219, 117, 65, 218, 62, 141, 215, 183, 7, 162, 235, 198, 201, 59, 141, 242, 192, 151, 78, 193, 164, 56, 186, 163, 135, 185, 20, 33, 59, 36, 35, 212, 158, 17, 203, 229, 52, 52, 144, 150, 45, 135, 95, 214, 211, 205, 220, 211, 180, 249, 67, 68, 139, 199, 132, 48, 22, 18, 122, 57, 119, 229, 236, 204, 220, 169, 84, 66, 158, 64, 5, 59, 216, 217, 92, 215, 72, 110, 214, 241, 120, 47, 139, 249, 182, 185, 194, 55 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0"), "student@mail.ru", new byte[] { 21, 86, 160, 31, 176, 210, 122, 48, 100, 152, 192, 191, 111, 200, 233, 246, 78, 90, 67, 182, 184, 245, 12, 218, 23, 150, 115, 227, 28, 45, 42, 53, 103, 96, 255, 38, 97, 66, 196, 160, 213, 127, 8, 145, 19, 6, 197, 85, 57, 167, 93, 131, 149, 146, 9, 46, 173, 0, 92, 145, 162, 57, 219, 68 }, new byte[] { 142, 74, 157, 30, 160, 18, 50, 173, 55, 203, 238, 35, 104, 68, 68, 83, 239, 95, 69, 19, 227, 110, 139, 10, 243, 134, 148, 6, 153, 69, 169, 87, 144, 218, 73, 208, 47, 42, 146, 252, 179, 17, 140, 25, 181, 188, 156, 153, 63, 108, 202, 225, 157, 120, 89, 175, 245, 71, 1, 167, 110, 240, 209, 199, 47, 254, 52, 98, 157, 143, 107, 204, 30, 78, 9, 135, 9, 183, 158, 212, 241, 18, 92, 154, 21, 0, 141, 255, 35, 226, 200, 165, 84, 133, 119, 61, 146, 251, 221, 144, 190, 207, 127, 140, 170, 196, 163, 76, 113, 41, 123, 29, 99, 169, 73, 18, 65, 173, 241, 154, 53, 110, 122, 191, 161, 223, 248, 232 }, "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "GroupId", "Name", "Semester" },
                values: new object[] { new Guid("3d697097-ffeb-45a5-9d71-544962735524"), new Guid("03829da0-798c-4d92-a81c-6c854688df1e"), "СУБД PostgreSQL", 0 });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "Id", "GroupId", "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("2b3aaf23-8968-473c-a7b9-71ab5110bbc2"), new Guid("03829da0-798c-4d92-a81c-6c854688df1e"), new Guid("03f4c303-c131-4233-8297-a040fb67c220"), new Guid("1970342a-0249-45be-b3ea-901fd76cfeea") },
                    { new Guid("90440bae-b30c-43c3-ab6a-336e7c2e3737"), new Guid("3b91b3b9-fafe-4986-a7f3-c1e09025e569"), new Guid("03f4c303-c131-4233-8297-a040fb67c220"), new Guid("1970342a-0249-45be-b3ea-901fd76cfeea") },
                    { new Guid("d80cf5b2-b858-410f-8028-a5a32b93b1be"), new Guid("03829da0-798c-4d92-a81c-6c854688df1e"), new Guid("6cb0202b-2e70-4475-b574-4eca31f5c440"), new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0") }
                });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("03f4c303-c131-4233-8297-a040fb67c220"), new Guid("1970342a-0249-45be-b3ea-901fd76cfeea") },
                    { new Guid("03f4c303-c131-4233-8297-a040fb67c220"), new Guid("761c31e6-202f-420f-8ec3-0bb178544caa") },
                    { new Guid("6cb0202b-2e70-4475-b574-4eca31f5c440"), new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0") },
                    { new Guid("eb040d7a-ad98-4ab4-ab25-3d3e4edb46f0"), new Guid("27c926f1-a755-484c-acd5-d4b9f573da6b") },
                    { new Guid("eb040d7a-ad98-4ab4-ab25-3d3e4edb46f0"), new Guid("761c31e6-202f-420f-8ec3-0bb178544caa") }
                });

            migrationBuilder.InsertData(
                table: "LabWorks",
                columns: new[] { "Id", "MaximumScore", "Number", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("75b503f5-2077-4ecb-afcc-8f83ce0951d0"), 10f, 1, new Guid("3d697097-ffeb-45a5-9d71-544962735524") },
                    { new Guid("81a353ba-8d2a-4e68-b8db-a9921bc39971"), 10f, 4, new Guid("3d697097-ffeb-45a5-9d71-544962735524") },
                    { new Guid("b27784ae-fbd6-4f29-ba7f-e56256de2782"), 10f, 3, new Guid("3d697097-ffeb-45a5-9d71-544962735524") },
                    { new Guid("dbc944fa-ee11-4302-b1e6-3ac0550a0491"), 10f, 2, new Guid("3d697097-ffeb-45a5-9d71-544962735524") }
                });

            migrationBuilder.InsertData(
                table: "TeacherSubject",
                columns: new[] { "SubjectId", "UserId" },
                values: new object[] { new Guid("3d697097-ffeb-45a5-9d71-544962735524"), new Guid("1970342a-0249-45be-b3ea-901fd76cfeea") });

            migrationBuilder.InsertData(
                table: "LabWorkStatuses",
                columns: new[] { "Id", "IsCompleted", "LabWorkId", "UserId" },
                values: new object[,]
                {
                    { new Guid("598dd563-f482-464a-930d-721a3a9d1649"), false, new Guid("75b503f5-2077-4ecb-afcc-8f83ce0951d0"), new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0") },
                    { new Guid("e8ace02d-e330-407d-b2ae-21256251bff3"), false, new Guid("b27784ae-fbd6-4f29-ba7f-e56256de2782"), new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0") },
                    { new Guid("f46af7e1-2abb-49a9-bc49-683ec1b3e4bb"), false, new Guid("81a353ba-8d2a-4e68-b8db-a9921bc39971"), new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0") },
                    { new Guid("fc15ee48-ed1a-4155-9022-f1c3ba2b789a"), false, new Guid("dbc944fa-ee11-4302-b1e6-3ac0550a0491"), new Guid("87340bc4-d2cc-4061-841c-a421d6c2a7f0") }
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
                filter: "\"RoleId\" = '6cb0202b-2e70-4475-b574-4eca31f5c440'");

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
