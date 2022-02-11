using Microsoft.EntityFrameworkCore.Migrations;

namespace MHRSLite_DAL.Migrations
{
    public partial class BaseIdUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "Hospitals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "HospitalClinics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "Districts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "Clinics",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "Cities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "AppointmentHours",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GetT",
                table: "Appointment",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Hospitals",
                newName: "GetT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "HospitalClinics",
                newName: "GetT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Districts",
                newName: "GetT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Clinics",
                newName: "GetT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cities",
                newName: "GetT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AppointmentHours",
                newName: "GetT");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Appointment",
                newName: "GetT");
        }
    }
}
