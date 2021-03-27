using Microsoft.EntityFrameworkCore.Migrations;

namespace Initial_Clean_Architecture.Data.Migrations
{
    public partial class updateLog2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Logs",
                newName: "TraceIdentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TraceIdentifier",
                table: "Logs",
                newName: "ActivityId");
        }
    }
}
