using Microsoft.EntityFrameworkCore.Migrations;

namespace webapi.Migrations
{
    public partial class V7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Decks_User_UserID",
                table: "Decks");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Game_GameID",
                table: "User");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Mage_MageID",
                table: "User");

            migrationBuilder.DropTable(
                name: "UserMageGame");

            migrationBuilder.DropIndex(
                name: "IX_User_GameID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_MageID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Decks_UserID",
                table: "Decks");

            migrationBuilder.DropColumn(
                name: "GameID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "MageID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Decks");

            migrationBuilder.CreateTable(
                name: "PlayerState",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false),
                    MageID = table.Column<int>(type: "int", nullable: false),
                    DeckID = table.Column<int>(type: "int", nullable: false),
                    ManaPoints = table.Column<int>(type: "int", nullable: false),
                    HealthPoints = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerState", x => new { x.GameID, x.UserID });
                    table.ForeignKey(
                        name: "FK_PlayerState_Decks_DeckID",
                        column: x => x.DeckID,
                        principalTable: "Decks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerState_Game_GameID",
                        column: x => x.GameID,
                        principalTable: "Game",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerState_Mage_MageID",
                        column: x => x.MageID,
                        principalTable: "Mage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerState_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_DeckID",
                table: "PlayerState",
                column: "DeckID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_MageID",
                table: "PlayerState",
                column: "MageID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlayerState_UserID",
                table: "PlayerState",
                column: "UserID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerState");

            migrationBuilder.AddColumn<int>(
                name: "GameID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MageID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Decks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserMageGame",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameID = table.Column<int>(type: "int", nullable: false),
                    HealthPoints = table.Column<int>(type: "int", nullable: false),
                    MageID = table.Column<int>(type: "int", nullable: false),
                    ManaPoints = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMageGame", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_GameID",
                table: "User",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_User_MageID",
                table: "User",
                column: "MageID");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_UserID",
                table: "Decks",
                column: "UserID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Decks_User_UserID",
                table: "Decks",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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
    }
}
