using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineClasses.Migrations
{
    public partial class UpdateWithGameSquareTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "XCoord",
                table: "GamePiece");

            migrationBuilder.DropColumn(
                name: "YCoord",
                table: "GamePiece");

            migrationBuilder.AddColumn<int>(
                name: "GameSquareId",
                table: "GamePiece",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GameSquare",
                columns: table => new
                {
                    GameSquareID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(nullable: true),
                    StartingSquare = table.Column<bool>(nullable: false),
                    EndSquare = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSquare", x => x.GameSquareID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamePiece_GameSquareId",
                table: "GamePiece",
                column: "GameSquareId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePiece_GameSquare_GameSquareId",
                table: "GamePiece",
                column: "GameSquareId",
                principalTable: "GameSquare",
                principalColumn: "GameSquareID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePiece_GameSquare_GameSquareId",
                table: "GamePiece");

            migrationBuilder.DropTable(
                name: "GameSquare");

            migrationBuilder.DropIndex(
                name: "IX_GamePiece_GameSquareId",
                table: "GamePiece");

            migrationBuilder.DropColumn(
                name: "GameSquareId",
                table: "GamePiece");

            migrationBuilder.AddColumn<int>(
                name: "XCoord",
                table: "GamePiece",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YCoord",
                table: "GamePiece",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
