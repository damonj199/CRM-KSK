using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddedTrainingAndUpdateAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fK_schedules_trainers_trainerId",
                table: "schedules");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "ScheduleClients");

            migrationBuilder.DropIndex(
                name: "iX_schedules_trainerId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "specialization",
                table: "trainers");

            migrationBuilder.DropColumn(
                name: "trainerId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "typeTrainings",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "levelOfTraining",
                table: "clients");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "dateEnd",
                table: "memberships",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<int>(
                name: "amountTraining",
                table: "memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "typeTrainings",
                table: "memberships",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "training",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    typeTrainings = table.Column<int>(type: "integer", nullable: false),
                    trainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    scheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_training", x => x.id);
                    table.ForeignKey(
                        name: "fK_training_schedules_scheduleId",
                        column: x => x.scheduleId,
                        principalTable: "schedules",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_training_trainers_trainerId",
                        column: x => x.trainerId,
                        principalTable: "trainers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "clientTraining",
                columns: table => new
                {
                    clientsId = table.Column<Guid>(type: "uuid", nullable: false),
                    trainingsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_clientTraining", x => new { x.clientsId, x.trainingsId });
                    table.ForeignKey(
                        name: "fK_clientTraining_clients_clientsId",
                        column: x => x.clientsId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_clientTraining_training_trainingsId",
                        column: x => x.trainingsId,
                        principalTable: "training",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_clientTraining_trainingsId",
                table: "clientTraining",
                column: "trainingsId");

            migrationBuilder.CreateIndex(
                name: "iX_training_scheduleId",
                table: "training",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "iX_training_trainerId",
                table: "training",
                column: "trainerId");

            migrationBuilder.AddForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients");

            migrationBuilder.DropTable(
                name: "clientTraining");

            migrationBuilder.DropTable(
                name: "training");

            migrationBuilder.DropColumn(
                name: "amountTraining",
                table: "memberships");

            migrationBuilder.DropColumn(
                name: "typeTrainings",
                table: "memberships");

            migrationBuilder.AddColumn<string>(
                name: "specialization",
                table: "trainers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "trainerId",
                table: "schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "typeTrainings",
                table: "schedules",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "dateEnd",
                table: "memberships",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddColumn<string>(
                name: "levelOfTraining",
                table: "clients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: true),
                    membershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    paymentMethod = table.Column<string>(type: "text", nullable: false),
                    summa = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_payment", x => x.id);
                    table.ForeignKey(
                        name: "fK_payment_clients_clientId",
                        column: x => x.clientId,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fK_payment_memberships_membershipId",
                        column: x => x.membershipId,
                        principalTable: "memberships",
                        principalColumn: "id");
                });

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
                name: "iX_schedules_trainerId",
                table: "schedules",
                column: "trainerId");

            migrationBuilder.CreateIndex(
                name: "iX_payment_clientId",
                table: "payment",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "iX_payment_membershipId",
                table: "payment",
                column: "membershipId");

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

            migrationBuilder.AddForeignKey(
                name: "fK_schedules_trainers_trainerId",
                table: "schedules",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
