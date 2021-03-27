using Microsoft.EntityFrameworkCore.Migrations;

namespace Initial_Clean_Architecture.Data.Migrations
{
    public partial class updatelog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Class",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "ActivityId",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Logs",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ResponseStatusCode",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResponseStatusMessage",
                table: "Logs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ResponseStatusCode",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "ResponseStatusMessage",
                table: "Logs");

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Logs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
