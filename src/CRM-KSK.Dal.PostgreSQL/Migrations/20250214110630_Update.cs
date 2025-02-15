using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients");

            migrationBuilder.DropForeignKey(
                name: "fK_clientTraining_clients_clientsId",
                table: "clientTraining");

            migrationBuilder.DropForeignKey(
                name: "fK_clientTraining_training_trainingsId",
                table: "clientTraining");

            migrationBuilder.DropForeignKey(
                name: "fK_memberships_clients_clientId",
                table: "memberships");

            migrationBuilder.DropForeignKey(
                name: "fK_training_schedules_scheduleId",
                table: "training");

            migrationBuilder.DropForeignKey(
                name: "fK_training_trainers_trainerId",
                table: "training");

            migrationBuilder.DropPrimaryKey(
                name: "pK_training",
                table: "training");

            migrationBuilder.DropPrimaryKey(
                name: "pK_memberships",
                table: "memberships");

            migrationBuilder.DropPrimaryKey(
                name: "pK_clients",
                table: "clients");

            migrationBuilder.RenameTable(
                name: "training",
                newName: "trainings");

            migrationBuilder.RenameTable(
                name: "memberships",
                newName: "membership");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "client");

            migrationBuilder.RenameIndex(
                name: "iX_training_trainerId",
                table: "trainings",
                newName: "iX_trainings_trainerId");

            migrationBuilder.RenameIndex(
                name: "iX_training_scheduleId",
                table: "trainings",
                newName: "iX_trainings_scheduleId");

            migrationBuilder.RenameIndex(
                name: "iX_memberships_clientId",
                table: "membership",
                newName: "iX_membership_clientId");

            migrationBuilder.RenameIndex(
                name: "iX_clients_trainerId",
                table: "client",
                newName: "iX_client_trainerId");

            migrationBuilder.AddPrimaryKey(
                name: "pK_trainings",
                table: "trainings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_membership",
                table: "membership",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_client",
                table: "client",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_client_trainers_trainerId",
                table: "client",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fK_clientTraining_client_clientsId",
                table: "clientTraining",
                column: "clientsId",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_clientTraining_trainings_trainingsId",
                table: "clientTraining",
                column: "trainingsId",
                principalTable: "trainings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_membership_client_clientId",
                table: "membership",
                column: "clientId",
                principalTable: "client",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_trainings_schedules_scheduleId",
                table: "trainings",
                column: "scheduleId",
                principalTable: "schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_trainings_trainers_trainerId",
                table: "trainings",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fK_client_trainers_trainerId",
                table: "client");

            migrationBuilder.DropForeignKey(
                name: "fK_clientTraining_client_clientsId",
                table: "clientTraining");

            migrationBuilder.DropForeignKey(
                name: "fK_clientTraining_trainings_trainingsId",
                table: "clientTraining");

            migrationBuilder.DropForeignKey(
                name: "fK_membership_client_clientId",
                table: "membership");

            migrationBuilder.DropForeignKey(
                name: "fK_trainings_schedules_scheduleId",
                table: "trainings");

            migrationBuilder.DropForeignKey(
                name: "fK_trainings_trainers_trainerId",
                table: "trainings");

            migrationBuilder.DropPrimaryKey(
                name: "pK_trainings",
                table: "trainings");

            migrationBuilder.DropPrimaryKey(
                name: "pK_membership",
                table: "membership");

            migrationBuilder.DropPrimaryKey(
                name: "pK_client",
                table: "client");

            migrationBuilder.RenameTable(
                name: "trainings",
                newName: "training");

            migrationBuilder.RenameTable(
                name: "membership",
                newName: "memberships");

            migrationBuilder.RenameTable(
                name: "client",
                newName: "clients");

            migrationBuilder.RenameIndex(
                name: "iX_trainings_trainerId",
                table: "training",
                newName: "iX_training_trainerId");

            migrationBuilder.RenameIndex(
                name: "iX_trainings_scheduleId",
                table: "training",
                newName: "iX_training_scheduleId");

            migrationBuilder.RenameIndex(
                name: "iX_membership_clientId",
                table: "memberships",
                newName: "iX_memberships_clientId");

            migrationBuilder.RenameIndex(
                name: "iX_client_trainerId",
                table: "clients",
                newName: "iX_clients_trainerId");

            migrationBuilder.AddPrimaryKey(
                name: "pK_training",
                table: "training",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_memberships",
                table: "memberships",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pK_clients",
                table: "clients",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fK_clients_trainers_trainerId",
                table: "clients",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fK_clientTraining_clients_clientsId",
                table: "clientTraining",
                column: "clientsId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_clientTraining_training_trainingsId",
                table: "clientTraining",
                column: "trainingsId",
                principalTable: "training",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_memberships_clients_clientId",
                table: "memberships",
                column: "clientId",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_training_schedules_scheduleId",
                table: "training",
                column: "scheduleId",
                principalTable: "schedules",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fK_training_trainers_trainerId",
                table: "training",
                column: "trainerId",
                principalTable: "trainers",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
