using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntitis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    passwordHash = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_admins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trainers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: true),
                    lastName = table.Column<string>(type: "text", nullable: true),
                    surname = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    specialization = table.Column<string>(type: "text", nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_trainers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    time = table.Column<TimeSpan>(type: "interval", nullable: false),
                    trainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: false),
                    typeTrainings = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fK_schedules_trainers_trainerId",
                        column: x => x.trainerId,
                        principalTable: "trainers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    levelOfTraining = table.Column<string>(type: "text", nullable: false),
                    parentName = table.Column<string>(type: "text", nullable: true),
                    parentPhone = table.Column<string>(type: "text", nullable: true),
                    trainerId = table.Column<Guid>(type: "uuid", nullable: true),
                    scheduleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_clients", x => x.id);
                    table.ForeignKey(
                        name: "fK_clients_schedules_scheduleId",
                        column: x => x.scheduleId,
                        principalTable: "schedules",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fK_clients_trainers_trainerId",
                        column: x => x.trainerId,
                        principalTable: "trainers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "memberships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: false),
                    dateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    dateEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    statusMembership = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_memberships", x => x.id);
                    table.ForeignKey(
                        name: "fK_memberships_clients_clientId",
                        column: x => x.clientId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    summa = table.Column<decimal>(type: "numeric", nullable: false),
                    paymentMethod = table.Column<string>(type: "text", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: true),
                    membershipId = table.Column<Guid>(type: "uuid", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "iX_clients_scheduleId",
                table: "clients",
                column: "scheduleId");

            migrationBuilder.CreateIndex(
                name: "iX_clients_trainerId",
                table: "clients",
                column: "trainerId");

            migrationBuilder.CreateIndex(
                name: "iX_memberships_clientId",
                table: "memberships",
                column: "clientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "iX_payment_clientId",
                table: "payment",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "iX_payment_membershipId",
                table: "payment",
                column: "membershipId");

            migrationBuilder.CreateIndex(
                name: "iX_schedules_trainerId",
                table: "schedules",
                column: "trainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "memberships");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "trainers");
        }
    }
}
