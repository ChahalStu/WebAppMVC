using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clinic",
                columns: table => new
                {
                    ClinicID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClinicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clinic", x => x.ClinicID);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientEmployerCellNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isPatientStudent = table.Column<bool>(type: "bit", nullable: false),
                    School = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Teacher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FathersName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherWorkPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherCellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherCellphone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherWorkPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherDoctorDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyDoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyDoctorInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyDoctorContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PsychologistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PsychologistInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PsychologistContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialWorkerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialWorkerInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SocialWorkerContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationalTherapist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationalTherapistName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationalTherapistInstitution = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OccupationalTherapistContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalRecord",
                columns: table => new
                {
                    ClinicalRecordID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disorder = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalContactCommenced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClinicalContactTerminated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RelevantInformation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TutorEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Clinician = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessmentFindings = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Referral = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    History = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicID = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalRecord", x => x.ClinicalRecordID);
                    table.ForeignKey(
                        name: "FK_ClinicalRecord_Clinic_ClinicID",
                        column: x => x.ClinicID,
                        principalTable: "Clinic",
                        principalColumn: "ClinicID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClinicalRecord_Patient_PatientID",
                        column: x => x.PatientID,
                        principalTable: "Patient",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecord_ClinicID",
                table: "ClinicalRecord",
                column: "ClinicID");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecord_PatientID",
                table: "ClinicalRecord",
                column: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClinicalRecord");

            migrationBuilder.DropTable(
                name: "Clinic");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
