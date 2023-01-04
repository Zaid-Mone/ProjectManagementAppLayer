using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementBusinessLayer.Migrations
{
    public partial class RemoveInvoicePaymentTerm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePaymentTerms");

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "PaymentTerms",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "PaymentTerms");

            migrationBuilder.CreateTable(
                name: "InvoicePaymentTerms",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvoiceId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PaymentTermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentTerms", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentTerms_Invoices_InvoiceId1",
                        column: x => x.InvoiceId1,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentTerms_PaymentTerms_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentTerms_InvoiceId1",
                table: "InvoicePaymentTerms",
                column: "InvoiceId1");

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentTerms_PaymentTermId",
                table: "InvoicePaymentTerms",
                column: "PaymentTermId");
        }
    }
}
