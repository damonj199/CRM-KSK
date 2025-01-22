using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntitisAndEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_schedules_clients_clientId",
                table: "schedules");

            migrationBuilder.DropForeignKey(
                name: "fK_schedules_trainers_trainerId",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "iX_schedules_clientId",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "iX_schedules_trainerId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "clientId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "clientName",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "day",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "trainerId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "trainerName",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "trainingType",
                table: "schedules");

            migrationBuilder.RenameColumn(
                name: "time",
                table: "schedules",
                newName: "dateTime");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "trainings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "typeTrainings",
                table: "trainings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "clientNameId",
                table: "schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "trainerNameId",
                table: "schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "levelOfTraining",
                table: "clients",
                type: "text",
                nullable: false,
                defaultValue: "");

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
                    clientId = table.Column<Guid>(type: "uuid", nullable: false),
                    membershipId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_payment", x => x.id);
                    table.ForeignKey(
                        name: "fK_payment_clients_clientId",
                        column: x => x.clientId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fK_payment_memberships_membershipId",
                        column: x => x.membershipId,
                        principalTable: "memberships",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "iX_schedules_clientNameId",
                table: "schedules",
                column: "clientNameId");

            migrationBuilder.CreateIndex(
                name: "iX_schedules_trainerNameId",
                table: "schedules",
                column: "trainerNameId");

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

            migrationBuilder.AddForeignKey(
                name: "fK_schedules_clients_clientNameId",
                table: "schedules",
                column: "clientNameId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_schedules_trainers_trainerNameId",
                table: "schedules",
                column: "trainerNameId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_schedules_clients_clientNameId",
                table: "schedules");

            migrationBuilder.DropForeignKey(
                name: "fK_schedules_trainers_trainerNameId",
                table: "schedules");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "memberships");

            migrationBuilder.DropIndex(
                name: "iX_schedules_clientNameId",
                table: "schedules");

            migrationBuilder.DropIndex(
                name: "iX_schedules_trainerNameId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "typeTrainings",
                table: "trainings");

            migrationBuilder.DropColumn(
                name: "clientNameId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "trainerNameId",
                table: "schedules");

            migrationBuilder.DropColumn(
                name: "levelOfTraining",
                table: "clients");

            migrationBuilder.RenameColumn(
                name: "dateTime",
                table: "schedules",
                newName: "time");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "trainings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "clientId",
                table: "schedules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "clientName",
                table: "schedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "day",
                table: "schedules",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "trainerId",
                table: "schedules",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "trainerName",
                table: "schedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "trainingType",
                table: "schedules",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "iX_schedules_clientId",
                table: "schedules",
                column: "clientId");

            migrationBuilder.CreateIndex(
                name: "iX_schedules_trainerId",
                table: "schedules",
                column: "trainerId");

            migrationBuilder.AddForeignKey(
                name: "fK_schedules_clients_clientId",
                table: "schedules",
                column: "clientId",
                principalTable: "clients",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_schedules_trainers_trainerId",
                table: "schedules",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id");
        }
    }
}
