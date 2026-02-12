using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMCH.Migrations
{
    /// <inheritdoc />
    public partial class UpdateHRAgencyWithNewFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "HRAgencies");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "HRAgencies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "HRAgencies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "HRAgencies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsWebloginEnabled",
                table: "HRAgencies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPhone",
                table: "HRAgencies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPhone",
                table: "HRAgencies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "HRAgencies",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WebsiteURL",
                table: "HRAgencies",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "IsWebloginEnabled",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "PrimaryPhone",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "SecondaryPhone",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "State",
                table: "HRAgencies");

            migrationBuilder.DropColumn(
                name: "WebsiteURL",
                table: "HRAgencies");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "HRAgencies",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }
    }
}
