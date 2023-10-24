using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meet.Migrations
{
    public partial class staterestriction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "96cce2a0-3560-4ba0-8d45-cec7c3da3b5d");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6fa6d418-6913-4f12-8000-d9a6453dc0c7", "9c129ad4-c6a2-41fe-9623-7427691a9e85", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6fa6d418-6913-4f12-8000-d9a6453dc0c7");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Clients",
                type: "nvarchar(2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "96cce2a0-3560-4ba0-8d45-cec7c3da3b5d", "cff6e9d0-f40c-4003-be2a-80836469371d", "CarGuy", "CARGUY" });
        }
    }
}
