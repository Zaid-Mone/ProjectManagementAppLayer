using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementBusinessLayer.Migrations
{
    public partial class addInvoicePaymentTermsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InvoicePaymentTerms",
                columns: table => new
                {
                    InvoiceId = table.Column<Guid>(nullable: false),
                    PaymentTermId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePaymentTerms", x => new { x.InvoiceId, x.PaymentTermId });
                    table.ForeignKey(
                        name: "FK_InvoicePaymentTerms_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoicePaymentTerms_PaymentTerms_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "PaymentTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoicePaymentTerms_PaymentTermId",
                table: "InvoicePaymentTerms",
                column: "PaymentTermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoicePaymentTerms");
        }
    }
}
