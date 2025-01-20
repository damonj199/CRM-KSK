using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMagrations : Migration
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
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    specialization = table.Column<string>(type: "text", nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_trainers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trainings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    trainerId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_trainings", x => x.id);
                    table.ForeignKey(
                        name: "fK_trainings_trainers_trainerId",
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
                    parentName = table.Column<string>(type: "text", nullable: true),
                    parentPhone = table.Column<string>(type: "text", nullable: true),
                    trainerId = table.Column<Guid>(type: "uuid", nullable: true),
                    trainingId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_clients", x => x.id);
                    table.ForeignKey(
                        name: "fK_clients_trainers_trainerId",
                        column: x => x.trainerId,
                        principalTable: "trainers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fK_clients_trainings_trainingId",
                        column: x => x.trainingId,
                        principalTable: "trainings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    day = table.Column<string>(type: "text", nullable: true),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    trainerName = table.Column<string>(type: "text", nullable: false),
                    clientName = table.Column<string>(type: "text", nullable: false),
                    trainingType = table.Column<string>(type: "text", nullable: false),
                    clientId = table.Column<Guid>(type: "uuid", nullable: true),
                    trainerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_schedules", x => x.id);
                    table.ForeignKey(
                        name: "fK_schedules_clients_clientId",
                        column: x => x.clientId,
                        principalTable: "clients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fK_schedules_trainers_trainerId",
                        column: x => x.trainerId,
                        principalTable: "trainers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "iX_clients_trainerId",
                table: "clients",
                column: "trainerId");

            migrationBuilder.CreateIndex(
                name: "iX_clients_trainingId",
                table: "clients",
                column: "trainingId");

            migrationBuilder.CreateIndex(
                name: "iX_schedules_clientId",
                table: "schedules",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "iX_schedules_trainerId",
                table: "schedules",
                column: "trainerId");

            migrationBuilder.CreateIndex(
                name: "iX_trainings_trainerId",
                table: "trainings",
                column: "trainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "trainings");

            migrationBuilder.DropTable(
                name: "trainers");
        }
    }
}
