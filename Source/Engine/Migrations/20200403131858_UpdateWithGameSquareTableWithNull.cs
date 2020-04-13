using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineClasses.Migrations
{
    public partial class UpdateWithGameSquareTableWithNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePiece_GameSquare_GameSquareId",
                table: "GamePiece");

            migrationBuilder.AlterColumn<int>(
                name: "GameSquareId",
                table: "GamePiece",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_GamePiece_GameSquare_GameSquareId",
                table: "GamePiece",
                column: "GameSquareId",
                principalTable: "GameSquare",
                principalColumn: "GameSquareId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamePiece_GameSquare_GameSquareId",
                table: "GamePiece");

            migrationBuilder.AlterColumn<int>(
                name: "GameSquareId",
                table: "GamePiece",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GamePiece_GameSquare_GameSquareId",
                table: "GamePiece",
                column: "GameSquareId",
                principalTable: "GameSquare",
                principalColumn: "GameSquareId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
