using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementBusinessLayer.Migrations
{
    public partial class addAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "PaymentTerms",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Invoices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Deliverables",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_AdminId",
                table: "Projects",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTerms_AdminId",
                table: "PaymentTerms",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_AdminId",
                table: "Invoices",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliverables_AdminId",
                table: "Deliverables",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliverables_AspNetUsers_AdminId",
                table: "Deliverables",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_AspNetUsers_AdminId",
                table: "Invoices",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTerms_AspNetUsers_AdminId",
                table: "PaymentTerms",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_AdminId",
                table: "Projects",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliverables_AspNetUsers_AdminId",
                table: "Deliverables");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_AspNetUsers_AdminId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTerms_AspNetUsers_AdminId",
                table: "PaymentTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_AdminId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_AdminId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTerms_AdminId",
                table: "PaymentTerms");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_AdminId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Deliverables_AdminId",
                table: "Deliverables");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "PaymentTerms");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Deliverables");
        }
    }
}
