using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QMCH.Migrations
{
    /// <inheritdoc />
    public partial class QMCH0000012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedAgents",
                table: "JobOrders");

            migrationBuilder.DropColumn(
                name: "Documents",
                table: "JobOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedAgents",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Documents",
                table: "JobOrders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
