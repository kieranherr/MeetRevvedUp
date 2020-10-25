using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet.Migrations
{
    public partial class AddComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d0c5be5-4fa8-47d9-97df-a094fe5435b5");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6dab57cd-5dc7-4fdd-bd17-014ab4ffc6c2", "a2bc0dea-e2a8-4479-8a7e-0d8e6bad2f3f", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dab57cd-5dc7-4fdd-bd17-014ab4ffc6c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d0c5be5-4fa8-47d9-97df-a094fe5435b5", "2a34968e-2cc2-42cb-840a-7703930b9859", "CarGuy", "CARGUY" });
        }
    }
}
