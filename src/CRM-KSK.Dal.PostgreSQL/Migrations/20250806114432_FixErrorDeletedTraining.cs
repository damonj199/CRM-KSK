using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class FixErrorDeletedTraining : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_membershipDeductionLogs_memberships_membershipId",
                table: "membershipDeductionLogs");

            migrationBuilder.DropForeignKey(
                name: "fK_membershipDeductionLogs_trainings_trainingId",
                table: "membershipDeductionLogs");

            migrationBuilder.DropForeignKey(
                name: "fK_trainings_trainers_trainerId",
                table: "trainings");

            migrationBuilder.AlterColumn<Guid>(
                name: "membershipId",
                table: "membershipDeductionLogs",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fK_membershipDeductionLogs_memberships_membershipId",
                table: "membershipDeductionLogs",
                column: "membershipId",
                principalTable: "memberships",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fK_membershipDeductionLogs_trainings_trainingId",
                table: "membershipDeductionLogs",
                column: "trainingId",
                principalTable: "trainings",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fK_trainings_trainers_trainerId",
                table: "trainings",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_membershipDeductionLogs_memberships_membershipId",
                table: "membershipDeductionLogs");

            migrationBuilder.DropForeignKey(
                name: "fK_membershipDeductionLogs_trainings_trainingId",
                table: "membershipDeductionLogs");

            migrationBuilder.DropForeignKey(
                name: "fK_trainings_trainers_trainerId",
                table: "trainings");

            migrationBuilder.AlterColumn<Guid>(
                name: "membershipId",
                table: "membershipDeductionLogs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fK_membershipDeductionLogs_memberships_membershipId",
                table: "membershipDeductionLogs",
                column: "membershipId",
                principalTable: "memberships",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fK_membershipDeductionLogs_trainings_trainingId",
                table: "membershipDeductionLogs",
                column: "trainingId",
                principalTable: "trainings",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fK_trainings_trainers_trainerId",
                table: "trainings",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
