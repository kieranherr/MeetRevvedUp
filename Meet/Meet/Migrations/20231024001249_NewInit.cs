using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meet.Migrations
{
    public partial class NewInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fa6d418-6913-4f12-8000-d9a6453dc0c7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9863ccd-54e6-4e5e-b190-222e75b980a7", "2e414988-96ab-421b-85ac-457a5e7c4921", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9863ccd-54e6-4e5e-b190-222e75b980a7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fa6d418-6913-4f12-8000-d9a6453dc0c7", "9c129ad4-c6a2-41fe-9623-7427691a9e85", "CarGuy", "CARGUY" });
        }
    }
}
