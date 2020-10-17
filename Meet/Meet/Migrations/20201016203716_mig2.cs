using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22f9ccd3-93f8-4424-983d-ea89062f742b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dfb672c-be41-4e17-8fce-0d38698483e9", "28ca3cb0-f3b6-4209-b3a9-d903805ed888", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5dfb672c-be41-4e17-8fce-0d38698483e9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "22f9ccd3-93f8-4424-983d-ea89062f742b", "728537fe-32c8-4f9a-b840-12dd14d0d970", "CarGuy", "CARGUY" });
        }
    }
}
