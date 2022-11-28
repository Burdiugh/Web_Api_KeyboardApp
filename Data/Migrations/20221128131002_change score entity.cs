using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class changescoreentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Misses",
                table: "AppScores");

            migrationBuilder.AlterColumn<float>(
                name: "Speed",
                table: "AppScores",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Accuracy",
                table: "AppScores",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Errors",
                table: "AppScores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accuracy",
                table: "AppScores");

            migrationBuilder.DropColumn(
                name: "Errors",
                table: "AppScores");

            migrationBuilder.AlterColumn<int>(
                name: "Speed",
                table: "AppScores",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<float>(
                name: "Misses",
                table: "AppScores",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
