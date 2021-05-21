using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet.Migrations
{
    public partial class SQLInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0703d6c6-2d1c-490a-aae7-e0dbfc34b3d8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f64053b2-26eb-4b0c-9ea1-52f55d0ca6e5", "8ef62233-583f-440c-9023-41463c76adf5", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f64053b2-26eb-4b0c-9ea1-52f55d0ca6e5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0703d6c6-2d1c-490a-aae7-e0dbfc34b3d8", "387ba68e-926c-441b-8a6d-c5c47b8eb511", "CarGuy", "CARGUY" });
        }
    }
}
