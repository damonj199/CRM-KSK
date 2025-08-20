using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Update11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_client_trainers_trainerId",
                table: "client");

            migrationBuilder.DropIndex(
                name: "iX_client_trainerId",
                table: "client");

            migrationBuilder.DropColumn(
                name: "trainerId",
                table: "client");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "trainerId",
                table: "client",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "iX_client_trainerId",
                table: "client",
                column: "trainerId");

            migrationBuilder.AddForeignKey(
                name: "fK_client_trainers_trainerId",
                table: "client",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
