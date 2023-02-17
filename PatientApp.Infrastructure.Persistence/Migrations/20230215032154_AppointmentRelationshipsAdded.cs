using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientApp.Infrastructure.Persistence.Migrations
{
    public partial class AppointmentRelationshipsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Appointments_AppointmentId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Appointments_AppointmentId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_AppointmentId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_AppointmentId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Doctors");

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryResultId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Patients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AppointmentId",
                table: "Doctors",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LaboratoryResultId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AppointmentId",
                table: "Patients",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_AppointmentId",
                table: "Doctors",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Appointments_AppointmentId",
                table: "Doctors",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Appointments_AppointmentId",
                table: "Patients",
                column: "AppointmentId",
                principalTable: "Appointments",
                principalColumn: "Id");
        }
    }
}
