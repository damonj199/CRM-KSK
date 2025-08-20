using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class FixError : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clientTraining_client_clientsId",
                table: "clientTraining");

            migrationBuilder.DropForeignKey(
                name: "fK_membership_client_clientId",
                table: "membership");

            migrationBuilder.DropPrimaryKey(
                name: "pK_membership",
                table: "membership");

            migrationBuilder.DropPrimaryKey(
                name: "pK_client",
                table: "client");

            migrationBuilder.RenameTable(
                name: "membership",
                newName: "memberships");

            migrationBuilder.RenameTable(
                name: "client",
                newName: "clients");

            migrationBuilder.RenameIndex(
                name: "iX_membership_clientId",
                table: "memberships",
                newName: "iX_memberships_clientId");

            migrationBuilder.AddPrimaryKey(
                name: "pK_memberships",
                table: "memberships",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_clients",
                table: "clients",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_clientTraining_clients_clientsId",
                table: "clientTraining",
                column: "clientsId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_memberships_clients_clientId",
                table: "memberships",
                column: "clientId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clientTraining_clients_clientsId",
                table: "clientTraining");

            migrationBuilder.DropForeignKey(
                name: "fK_memberships_clients_clientId",
                table: "memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pK_memberships",
                table: "memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pK_clients",
                table: "clients");

            migrationBuilder.RenameTable(
                name: "memberships",
                newName: "membership");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "client");

            migrationBuilder.RenameIndex(
                name: "iX_memberships_clientId",
                table: "membership",
                newName: "iX_membership_clientId");

            migrationBuilder.AddPrimaryKey(
                name: "pK_membership",
                table: "membership",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_client",
                table: "client",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_clientTraining_client_clientsId",
                table: "clientTraining",
                column: "clientsId",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_membership_client_clientId",
                table: "membership",
                column: "clientId",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
