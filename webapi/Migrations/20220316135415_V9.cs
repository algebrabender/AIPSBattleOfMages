using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class V9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TurnOrder",
                table: "PlayerState",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TurnOrder",
                table: "PlayerState");
        }
    }
}
