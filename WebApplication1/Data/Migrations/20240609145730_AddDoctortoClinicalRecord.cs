using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctortoClinicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorID",
                table: "ClinicalRecord",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalRecord_DoctorID",
                table: "ClinicalRecord",
                column: "DoctorID");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalRecord_Doctor_DoctorID",
                table: "ClinicalRecord",
                column: "DoctorID",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalRecord_Doctor_DoctorID",
                table: "ClinicalRecord");

            migrationBuilder.DropIndex(
                name: "IX_ClinicalRecord_DoctorID",
                table: "ClinicalRecord");

            migrationBuilder.DropColumn(
                name: "DoctorID",
                table: "ClinicalRecord");
        }
    }
}
