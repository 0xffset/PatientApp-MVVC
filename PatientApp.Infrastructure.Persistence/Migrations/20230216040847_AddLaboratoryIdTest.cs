using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientApp.Infrastructure.Persistence.Migrations
{
    public partial class AddLaboratoryIdTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LaboratoryTestId",
                table: "LaboratoryResults",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaboratoryTestId",
                table: "LaboratoryResults");
        }
    }
}
