using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineClasses.Migrations
{
    public partial class NewInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "BoardSquareNumber",
                table: "GamePiece",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GamePieceNumber",
                table: "GamePiece",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoardSquareNumber",
                table: "GamePiece");

            migrationBuilder.DropColumn(
                name: "GamePieceNumber",
                table: "GamePiece");

            migrationBuilder.AddColumn<int>(
                name: "GameSquareId",
                table: "GamePiece",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GameSquare",
                columns: table => new
                {
                    GameSquareId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EndSquare = table.Column<bool>(type: "bit", nullable: false),
                    StartingSquare = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSquare", x => x.GameSquareId);
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
                principalColumn: "GameSquareId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
