using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class V8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerState_MageID",
                table: "PlayerState");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_MageID",
                table: "PlayerState",
                column: "MageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PlayerState_MageID",
                table: "PlayerState");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_MageID",
                table: "PlayerState",
                column: "MageID",
                unique: true);
        }
    }
}
