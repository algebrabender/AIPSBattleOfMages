using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Terrain_TerrainID",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Game_GameID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Mage_MageID",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "MageID",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GameID",
                table: "User",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TerrainID",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Terrain_TerrainID",
                table: "Game",
                column: "TerrainID",
                principalTable: "Terrain",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Game_GameID",
                table: "User",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Mage_MageID",
                table: "User",
                column: "MageID",
                principalTable: "Mage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Terrain_TerrainID",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Game_GameID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Mage_MageID",
                table: "User");

            migrationBuilder.AlterColumn<int>(
                name: "MageID",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GameID",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TerrainID",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Terrain_TerrainID",
                table: "Game",
                column: "TerrainID",
                principalTable: "Terrain",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Game_GameID",
                table: "User",
                column: "GameID",
                principalTable: "Game",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Mage_MageID",
                table: "User",
                column: "MageID",
                principalTable: "Mage",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
