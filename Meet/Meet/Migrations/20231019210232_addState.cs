using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meet.Migrations
{
    public partial class addState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72dea7b9-ebc9-40b7-aa3a-b509d67f8bb0");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "96cce2a0-3560-4ba0-8d45-cec7c3da3b5d", "cff6e9d0-f40c-4003-be2a-80836469371d", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96cce2a0-3560-4ba0-8d45-cec7c3da3b5d");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Clients");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "72dea7b9-ebc9-40b7-aa3a-b509d67f8bb0", "a3c322f9-5a6b-4e20-b770-d87f57046089", "CarGuy", "CARGUY" });
        }
    }
}
