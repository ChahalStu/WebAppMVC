using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPatientFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FamilyDoctorContactNo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "FamilyDoctorInstitution",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "FamilyDoctorName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "OccupationalTherapist",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "OccupationalTherapistContactNo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "OccupationalTherapistInstitution",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "OccupationalTherapistName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "OtherDoctorDetails",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PsychologistContactNo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PsychologistInstitution",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "PsychologistName",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "SocialWorkerContactNo",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "SocialWorkerInstitution",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "SocialWorkerName",
                table: "Patient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FamilyDoctorContactNo",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyDoctorInstitution",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FamilyDoctorName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccupationalTherapist",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccupationalTherapistContactNo",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccupationalTherapistInstitution",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OccupationalTherapistName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherDoctorDetails",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PsychologistContactNo",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PsychologistInstitution",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PsychologistName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialWorkerContactNo",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialWorkerInstitution",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SocialWorkerName",
                table: "Patient",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
