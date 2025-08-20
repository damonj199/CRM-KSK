using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsMorningMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isMorningMembership",
                table: "membershipDeductionLogs",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isMorningMembership",
                table: "membershipDeductionLogs");
        }
    }
}
