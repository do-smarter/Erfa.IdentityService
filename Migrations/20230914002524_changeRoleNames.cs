using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class changeRoleNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "025a89e0-ac47-41f7-9e0f-ff3430af9685");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b42ac32-df73-4b26-99c5-7653104f3a42");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73f00372-f36e-4c11-a8db-495c0d945ffa");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5a53f3ea-1a81-430a-b0eb-ebbf4059338f", "2", "ProdWorker", "PRODWORKER" },
                    { "6f7e6a96-81b8-4d3e-892c-a17140e84e36", "2", "HRAdmin", "HRADMIN" },
                    { "7ed688bb-09b1-4c3d-affc-37800aeb04d0", "1", "ProdAdmin", "PRODADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5a53f3ea-1a81-430a-b0eb-ebbf4059338f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f7e6a96-81b8-4d3e-892c-a17140e84e36");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ed688bb-09b1-4c3d-affc-37800aeb04d0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "025a89e0-ac47-41f7-9e0f-ff3430af9685", "2", "Production Worker", "PRODWORKER" },
                    { "3b42ac32-df73-4b26-99c5-7653104f3a42", "1", "Production Admin", "PRODADMIN" },
                    { "73f00372-f36e-4c11-a8db-495c0d945ffa", "2", "HR Admin", "HRADMIN" }
                });
        }
    }
}
