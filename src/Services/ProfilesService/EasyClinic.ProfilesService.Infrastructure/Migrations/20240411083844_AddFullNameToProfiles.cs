using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyClinic.ProfilesService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToProfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "ReceptionistProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "PatientProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "DoctorProfiles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistProfiles_FullName",
                table: "ReceptionistProfiles",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_FullName",
                table: "PatientProfiles",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_FullName",
                table: "DoctorProfiles",
                column: "FullName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ReceptionistProfiles_FullName",
                table: "ReceptionistProfiles");

            migrationBuilder.DropIndex(
                name: "IX_PatientProfiles_FullName",
                table: "PatientProfiles");

            migrationBuilder.DropIndex(
                name: "IX_DoctorProfiles_FullName",
                table: "DoctorProfiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "ReceptionistProfiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "PatientProfiles");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "DoctorProfiles");
        }
    }
}
