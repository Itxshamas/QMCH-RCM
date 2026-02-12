using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMCH.Migrations
{
    /// <inheritdoc />
    public partial class QMCH0005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmploymentStatus",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExperienceRange",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleToDirectApplicant",
                table: "JobOrders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredQualification",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequiredSkills",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmploymentStatus",
                table: "JobOrders");

            migrationBuilder.DropColumn(
                name: "ExperienceRange",
                table: "JobOrders");

            migrationBuilder.DropColumn(
                name: "IsVisibleToDirectApplicant",
                table: "JobOrders");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "JobOrders");

            migrationBuilder.DropColumn(
                name: "RequiredQualification",
                table: "JobOrders");

            migrationBuilder.DropColumn(
                name: "RequiredSkills",
                table: "JobOrders");
        }
    }
}
