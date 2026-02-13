using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMCH.Migrations
{
    /// <inheritdoc />
    public partial class QMCH0000123444444 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Resumes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriminalConviction = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CriminalConvictionDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallengesDuringWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChallengingWorkTypes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmploymentHistoryJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Contact1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Contact2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AvailabilityForImmediateWork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AvailabilityForNightShift = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AvailabilityForWeekendWork = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AvailabilityForOtherLocations = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PreferredEmploymentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AvailableFromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAvailableToStartImmediately = table.Column<bool>(type: "bit", nullable: false),
                    ReferencesJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkillsMatrixJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resumes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Resumes");
        }
    }
}
