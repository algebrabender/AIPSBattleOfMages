using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class V5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NumberInDeck",
                table: "Card",
                newName: "Ice");

            migrationBuilder.AddColumn<int>(
                name: "NumOfPlayers",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberInDeck",
                table: "CardDeck",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Air",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Earth",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fire",
                table: "Card",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumOfPlayers",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "NumberInDeck",
                table: "CardDeck");

            migrationBuilder.DropColumn(
                name: "Air",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Earth",
                table: "Card");

            migrationBuilder.DropColumn(
                name: "Fire",
                table: "Card");

            migrationBuilder.RenameColumn(
                name: "Ice",
                table: "Card",
                newName: "NumberInDeck");
        }
    }
}
