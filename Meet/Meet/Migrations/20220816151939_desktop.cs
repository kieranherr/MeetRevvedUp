using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet.Migrations
{
    public partial class desktop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f64053b2-26eb-4b0c-9ea1-52f55d0ca6e5");

            migrationBuilder.AddColumn<double>(
                name: "Lat",
                table: "Clients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Long",
                table: "Clients",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4d49d76c-7241-459a-9d1f-00fde9209f6d", "8f338441-34ac-4fff-be2b-cbc8322c6407", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d49d76c-7241-459a-9d1f-00fde9209f6d");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Long",
                table: "Clients");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f64053b2-26eb-4b0c-9ea1-52f55d0ca6e5", "8ef62233-583f-440c-9023-41463c76adf5", "CarGuy", "CARGUY" });
        }
    }
}
