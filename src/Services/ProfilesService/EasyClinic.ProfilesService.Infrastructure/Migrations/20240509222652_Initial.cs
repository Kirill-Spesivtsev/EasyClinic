﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyClinic.ProfilesService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalSpecializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalSpecializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionistProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionistProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DoctorProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CareerStartYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicalSpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeStatusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_EmployeeStatuses_EmployeeStatusId",
                        column: x => x.EmployeeStatusId,
                        principalTable: "EmployeeStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorProfiles_MedicalSpecializations_MedicalSpecializationId",
                        column: x => x.MedicalSpecializationId,
                        principalTable: "MedicalSpecializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_EmployeeStatusId",
                table: "DoctorProfiles",
                column: "EmployeeStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_FirstName",
                table: "DoctorProfiles",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_FullName",
                table: "DoctorProfiles",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_LastName",
                table: "DoctorProfiles",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorProfiles_MedicalSpecializationId",
                table: "DoctorProfiles",
                column: "MedicalSpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_FirstName",
                table: "PatientProfiles",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_FullName",
                table: "PatientProfiles",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProfiles_LastName",
                table: "PatientProfiles",
                column: "LastName");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistProfiles_FirstName",
                table: "ReceptionistProfiles",
                column: "FirstName");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistProfiles_FullName",
                table: "ReceptionistProfiles",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionistProfiles_LastName",
                table: "ReceptionistProfiles",
                column: "LastName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorProfiles");

            migrationBuilder.DropTable(
                name: "PatientProfiles");

            migrationBuilder.DropTable(
                name: "ReceptionistProfiles");

            migrationBuilder.DropTable(
                name: "EmployeeStatuses");

            migrationBuilder.DropTable(
                name: "MedicalSpecializations");
        }
    }
}
