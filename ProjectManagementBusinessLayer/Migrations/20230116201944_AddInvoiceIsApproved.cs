using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementBusinessLayer.Migrations
{
    public partial class AddInvoiceIsApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Invoices",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Invoices");
        }
    }
}
