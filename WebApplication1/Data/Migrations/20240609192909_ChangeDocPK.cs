using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDocPK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalRecord_Doctor_DoctorID",
                table: "ClinicalRecord");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Doctor",
                newName: "DoctorID");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorID",
                table: "ClinicalRecord",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalRecord_Doctor_DoctorID",
                table: "ClinicalRecord",
                column: "DoctorID",
                principalTable: "Doctor",
                principalColumn: "DoctorID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalRecord_Doctor_DoctorID",
                table: "ClinicalRecord");

            migrationBuilder.RenameColumn(
                name: "DoctorID",
                table: "Doctor",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorID",
                table: "ClinicalRecord",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalRecord_Doctor_DoctorID",
                table: "ClinicalRecord",
                column: "DoctorID",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
