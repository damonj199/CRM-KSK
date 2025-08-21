using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_KSK.Dal.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTypeStatusMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE memberships 
                ALTER COLUMN ""statusMembership"" TYPE smallint 
                USING (""statusMembership""::smallint)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "statusMembership",
                table: "memberships",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "smallint");
        }
    }
}
