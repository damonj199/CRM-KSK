using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ReplacedEntityWorkHorse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "workHorses");

            migrationBuilder.CreateTable(
                name: "horsesWorks",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    rowNumber = table.Column<int>(type: "integer", nullable: false),
                    startWeek = table.Column<DateOnly>(type: "date", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    contentText = table.Column<string>(type: "text", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_horsesWorks", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "horsesWorks");

            migrationBuilder.CreateTable(
                name: "workHorses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contentText = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    rowNumber = table.Column<int>(type: "integer", nullable: false),
                    weekStartDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pK_workHorses", x => x.id);
                });
        }
    }
}
