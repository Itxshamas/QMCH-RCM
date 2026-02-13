using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMCH.Migrations
{
    /// <inheritdoc />
    public partial class QMCH0000001234 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Applicants",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Applicants");
        }
    }
}
