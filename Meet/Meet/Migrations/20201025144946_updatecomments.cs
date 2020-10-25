using Microsoft.EntityFrameworkCore.Migrations;

namespace Meet.Migrations
{
    public partial class updatecomments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dab57cd-5dc7-4fdd-bd17-014ab4ffc6c2");

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentorsName = table.Column<string>(nullable: true),
                    CommentBody = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    MeetId = table.Column<int>(nullable: false),
                    carMeetMeetId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_CarMeets_carMeetMeetId",
                        column: x => x.carMeetMeetId,
                        principalTable: "CarMeets",
                        principalColumn: "MeetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77c29ff4-e6a3-4210-a3ab-8f4fa47c5111", "d4e69429-1fcb-4a91-af33-51c2be9d1d6f", "CarGuy", "CARGUY" });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_carMeetMeetId",
                table: "Comments",
                column: "carMeetMeetId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77c29ff4-e6a3-4210-a3ab-8f4fa47c5111");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6dab57cd-5dc7-4fdd-bd17-014ab4ffc6c2", "a2bc0dea-e2a8-4479-8a7e-0d8e6bad2f3f", "CarGuy", "CARGUY" });
        }
    }
}
