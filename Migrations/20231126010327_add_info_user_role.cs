using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Erfa.IdentityService.Migrations
{
    /// <inheritdoc />
    public partial class add_info_user_role : Migration
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
                    { "1fd6cccd-736d-4997-aa19-a1b080f0b0af", "2", "InfoUser", "InfoUser" },
                    { "5899f1f4-525d-44eb-8e26-ffa1ce54f689", "2", "ProdWorker", "PRODWORKER" },
                    { "6ca4629b-214e-4c58-ba0f-83bf5d5e04dd", "2", "Seller", "SELLER" },
                    { "a4d3ac75-d3e9-45a9-a1cf-b05532d53de1", "1", "ProdAdmin", "PRODADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fd6cccd-736d-4997-aa19-a1b080f0b0af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5899f1f4-525d-44eb-8e26-ffa1ce54f689");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6ca4629b-214e-4c58-ba0f-83bf5d5e04dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a4d3ac75-d3e9-45a9-a1cf-b05532d53de1");

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
