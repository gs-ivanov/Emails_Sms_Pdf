using Microsoft.EntityFrameworkCore.Migrations;

namespace Emails_Sms_Pdf.Data.Migrations
{
    public partial class AddIsActiveToTournamentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tournaments");
        }
    }
}
