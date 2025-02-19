using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitySchedule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clients_schedules_scheduleId",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients");

            migrationBuilder.DropIndex(
                name: "iX_clients_scheduleId",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "scheduleId",
                table: "clients");

            migrationBuilder.AlterColumn<Guid>(
                name: "trainerId",
                table: "clients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ScheduleClients",
                columns: table => new
                {
                    clientsId = table.Column<Guid>(type: "uuid", nullable: false),
                    schedulesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_ScheduleClients", x => new { x.clientsId, x.schedulesId });
                    table.ForeignKey(
                        name: "fK_ScheduleClients_clients_clientsId",
                        column: x => x.clientsId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_ScheduleClients_schedules_schedulesId",
                        column: x => x.schedulesId,
                        principalTable: "schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_ScheduleClients_schedulesId",
                table: "ScheduleClients",
                column: "schedulesId");

            migrationBuilder.AddForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients");

            migrationBuilder.DropTable(
                name: "ScheduleClients");

            migrationBuilder.AddColumn<Guid>(
                name: "clientId",
                table: "schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "trainerId",
                table: "clients",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "scheduleId",
                table: "clients",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "iX_clients_scheduleId",
                table: "clients",
                column: "scheduleId");

            migrationBuilder.AddForeignKey(
                name: "fK_clients_schedules_scheduleId",
                table: "clients",
                column: "scheduleId",
                principalTable: "schedules",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id");
        }
    }
}
