using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientApp.Infrastructure.Persistence.Migrations
{
    public partial class AppointmentRelationshipsLaboratoryResults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_LaboratoryResults_LaboratoryResultId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_LaboratoryResults_LaboratoryResultId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_LaboratoryResultId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_LaboratoryResultId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "LaboratoryResultId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "LaboratoryResultId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "LaboratoryResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "LaboratoryResults",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryResults_PatientId",
                table: "LaboratoryResults",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryResults_LaboratoryTests_PatientId",
                table: "LaboratoryResults",
                column: "PatientId",
                principalTable: "LaboratoryTests",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryResults_Patients_PatientId",
                table: "LaboratoryResults",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryResults_LaboratoryTests_PatientId",
                table: "LaboratoryResults");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryResults_Patients_PatientId",
                table: "LaboratoryResults");

            migrationBuilder.DropIndex(
                name: "IX_LaboratoryResults_PatientId",
                table: "LaboratoryResults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LaboratoryResults");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "LaboratoryResults");

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryResultId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LaboratoryResultId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_LaboratoryResultId",
                table: "Patients",
                column: "LaboratoryResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_LaboratoryResultId",
                table: "Appointments",
                column: "LaboratoryResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_LaboratoryResults_LaboratoryResultId",
                table: "Appointments",
                column: "LaboratoryResultId",
                principalTable: "LaboratoryResults",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_LaboratoryResults_LaboratoryResultId",
                table: "Patients",
                column: "LaboratoryResultId",
                principalTable: "LaboratoryResults",
                principalColumn: "Id");
        }
    }
}
