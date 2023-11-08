using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Meet.Migrations
{
    public partial class migrationsSUck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9863ccd-54e6-4e5e-b190-222e75b980a7");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "MeetTime",
                table: "CarMeets",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "MeetDate",
                table: "CarMeets",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "645a645b-4950-4ecc-b1e3-05faaa9477c0", "a9d4260b-358a-4a3b-85e9-75c01845107b", "CarGuy", "CARGUY" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "645a645b-4950-4ecc-b1e3-05faaa9477c0");

            migrationBuilder.AlterColumn<string>(
                name: "MeetTime",
                table: "CarMeets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AlterColumn<string>(
                name: "MeetDate",
                table: "CarMeets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b9863ccd-54e6-4e5e-b190-222e75b980a7", "2e414988-96ab-421b-85ac-457a5e7c4921", "CarGuy", "CARGUY" });
        }
    }
}
