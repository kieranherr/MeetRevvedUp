using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meet.Migrations
{
    public partial class new_init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f64053b2-26eb-4b0c-9ea1-52f55d0ca6e5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "72dea7b9-ebc9-40b7-aa3a-b509d67f8bb0", "a3c322f9-5a6b-4e20-b770-d87f57046089", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72dea7b9-ebc9-40b7-aa3a-b509d67f8bb0");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f64053b2-26eb-4b0c-9ea1-52f55d0ca6e5", "8ef62233-583f-440c-9023-41463c76adf5", "CarGuy", "CARGUY" });
        }
    }
}
