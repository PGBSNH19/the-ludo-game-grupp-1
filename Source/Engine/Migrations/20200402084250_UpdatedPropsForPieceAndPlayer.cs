using Microsoft.EntityFrameworkCore.Migrations;

namespace EngineClasses.Migrations
{
    public partial class UpdatedPropsForPieceAndPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "GamePiece");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Player",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAtBase",
                table: "GamePiece",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAtGoal",
                table: "GamePiece",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "IsAtBase",
                table: "GamePiece");

            migrationBuilder.DropColumn(
                name: "IsAtGoal",
                table: "GamePiece");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "GamePiece",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
