using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class updateStructureTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "iX_memberships_clientId",
                table: "memberships");

            migrationBuilder.CreateIndex(
                name: "iX_memberships_clientId",
                table: "memberships",
                column: "clientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "iX_memberships_clientId",
                table: "memberships");

            migrationBuilder.CreateIndex(
                name: "iX_memberships_clientId",
                table: "memberships",
                column: "clientId",
                unique: true);
        }
    }
}
