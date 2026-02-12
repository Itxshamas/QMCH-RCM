using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMCH.Migrations
{
    /// <inheritdoc />
    public partial class QMCH00013 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodGroupStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessUnit",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractFor",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FingerPrintStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmployeeReJoined",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastWorkingDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicalStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PRCIdentificationExpire",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PRCIdentificationIssued",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PRCRegistrationNumber",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PassportExpire",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PassportIssued",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNumber",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PoliceClearStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreliminaryAssessmentExpire",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PreliminaryAssessmentIssued",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QCHPLicenseExpire",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QCHPLicenseIssued",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QCHPLicenseNumber",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QatarIdExpire",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "QatarIdIssued",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QatarIdNumber",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QatarIdStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonInActive",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecruitmentAgency",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejoinedDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoleInSystem",
                table: "Employees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SCHLicenseStatus",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Employees",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TravellingDate",
                table: "Employees",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodGroupStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BusinessUnit",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractFor",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ContractStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "FingerPrintStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IsEmployeeReJoined",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "JoinDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "LastWorkingDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MedicalStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PRCIdentificationExpire",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PRCIdentificationIssued",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PRCRegistrationNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PassportExpire",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PassportIssued",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PassportNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PoliceClearStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PreliminaryAssessmentExpire",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PreliminaryAssessmentIssued",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QCHPLicenseExpire",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QCHPLicenseIssued",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QCHPLicenseNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QatarIdExpire",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QatarIdIssued",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QatarIdNumber",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "QatarIdStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ReasonInActive",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RecruitmentAgency",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RejoinedDate",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RoleInSystem",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SCHLicenseStatus",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TravellingDate",
                table: "Employees");
        }
    }
}
