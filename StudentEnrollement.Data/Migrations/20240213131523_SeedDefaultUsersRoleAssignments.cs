using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentEnrollement.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultUsersRoleAssignments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f76dff1-278e-49eb-99b1-4cbb70402d40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "833887ed-dc0b-4674-b657-067719d1f0c1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c841b8b5-e2c0-4ed1-90ed-072a95fdbc25", null, "Administrator", "ADMINISTRATOR" },
                    { "d34564b1-3686-4eef-8484-950c6c8a3de1", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "3f4631bd-f907-4409-b416-ba356312e659", 0, "1779a3ff-3e45-4d1c-8c6c-6fa23db69c74", null, "schooluser@localhost.com", true, "School", "User", false, null, "SCHOOLUSER@LOCALHOST.COM", "SCHOOLUSER@LOCALHOST.COM", "AQAAAAIAAYagAAAAEDo56dIsb2Bw2WLsh/2NKyyRFFjNnbx9SDPdGNyw2aijDRoEMEM93doK5IUgRKMhLg==", null, false, "1c30168d-f8f2-4701-96b0-a7cb909f2144", false, "schooluser@localhost.com" },
                    { "408aa945-3d84-4421-8342-7269ec64d949", 0, "9f08b06a-3657-47db-8906-7d6c3a6e2e41", null, "schooladmin@localhost.com", true, "School", "Admin", false, null, "SCHOOLADMIN@LOCALHOST.COM", "SCHOOLADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEKSYrGExcEAUGgM8pg2xUfKu51WTazZ/kvWANIUlKcudprEwiQH0HdT9lhnBjH6zww==", null, false, "d419ee7d-7812-47fb-96f0-5b77389af55a", false, "schooladmin@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "d34564b1-3686-4eef-8484-950c6c8a3de1", "3f4631bd-f907-4409-b416-ba356312e659" },
                    { "c841b8b5-e2c0-4ed1-90ed-072a95fdbc25", "408aa945-3d84-4421-8342-7269ec64d949" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d34564b1-3686-4eef-8484-950c6c8a3de1", "3f4631bd-f907-4409-b416-ba356312e659" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c841b8b5-e2c0-4ed1-90ed-072a95fdbc25", "408aa945-3d84-4421-8342-7269ec64d949" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c841b8b5-e2c0-4ed1-90ed-072a95fdbc25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d34564b1-3686-4eef-8484-950c6c8a3de1");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3f4631bd-f907-4409-b416-ba356312e659");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "408aa945-3d84-4421-8342-7269ec64d949");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f76dff1-278e-49eb-99b1-4cbb70402d40", null, "User", "USER" },
                    { "833887ed-dc0b-4674-b657-067719d1f0c1", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
