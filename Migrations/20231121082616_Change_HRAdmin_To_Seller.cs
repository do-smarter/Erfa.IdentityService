using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class Change_HRAdmin_To_Seller : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "3d7f2c1f-d766-4a10-9fbf-480a84a4df94", "2", "ProdWorker", "PRODWORKER" },
                    { "6dfe8574-1135-462a-a2ac-90bd82ee55e8", "1", "ProdAdmin", "PRODADMIN" },
                    { "9a14101b-7335-42cb-a848-2fff93d0a85d", "2", "Seller", "SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d7f2c1f-d766-4a10-9fbf-480a84a4df94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dfe8574-1135-462a-a2ac-90bd82ee55e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9a14101b-7335-42cb-a848-2fff93d0a85d");

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
    }
}
