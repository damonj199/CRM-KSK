using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddMembershipLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "membershipDeductionLogs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    deductionDate = table.Column<DateOnly>(type: "date", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: false),
                    membershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    scheduleId = table.Column<Guid>(type: "uuid", nullable: false),
                    trainingId = table.Column<Guid>(type: "uuid", nullable: false),
                    trainingType = table.Column<int>(type: "integer", nullable: false),
                    trainingsBeforeDeduction = table.Column<int>(type: "integer", nullable: false),
                    trainingsAfterDeduction = table.Column<int>(type: "integer", nullable: false),
                    membershipExpired = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_membershipDeductionLogs", x => x.id);
                    table.ForeignKey(
                        name: "fK_membershipDeductionLogs_clients_clientId",
                        column: x => x.clientId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_membershipDeductionLogs_memberships_membershipId",
                        column: x => x.membershipId,
                        principalTable: "memberships",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_membershipDeductionLogs_schedules_scheduleId",
                        column: x => x.scheduleId,
                        principalTable: "schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fK_membershipDeductionLogs_trainings_trainingId",
                        column: x => x.trainingId,
                        principalTable: "trainings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_clientId",
                table: "membershipDeductionLogs",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_deductionDate",
                table: "membershipDeductionLogs",
                column: "deductionDate");

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_membershipExpired",
                table: "membershipDeductionLogs",
                column: "membershipExpired");

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_membershipId",
                table: "membershipDeductionLogs",
                column: "membershipId");

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_scheduleId",
                table: "membershipDeductionLogs",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_trainingId",
                table: "membershipDeductionLogs",
                column: "trainingId");

            migrationBuilder.CreateIndex(
                name: "iX_membershipDeductionLogs_trainingType",
                table: "membershipDeductionLogs",
                column: "trainingType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "membershipDeductionLogs");
        }
    }
}
