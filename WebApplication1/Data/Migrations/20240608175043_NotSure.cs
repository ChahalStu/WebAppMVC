using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class NotSure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientID",
                table: "Doctor",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctor_PatientID",
                table: "Doctor",
                column: "PatientID");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctor_Patient_PatientID",
                table: "Doctor",
                column: "PatientID",
                principalTable: "Patient",
                principalColumn: "PatientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctor_Patient_PatientID",
                table: "Doctor");

            migrationBuilder.DropIndex(
                name: "IX_Doctor_PatientID",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "PatientID",
                table: "Doctor");
        }
    }
}
