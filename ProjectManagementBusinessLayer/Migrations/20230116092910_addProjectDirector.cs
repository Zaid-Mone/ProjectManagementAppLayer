using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManagementBusinessLayer.Migrations
{
    public partial class addProjectDirector : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectDirectorId",
                table: "Projects",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectDirectorId",
                table: "Projects",
                column: "ProjectDirectorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectDirectorId",
                table: "Projects",
                column: "ProjectDirectorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_ProjectDirectorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectDirectorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectDirectorId",
                table: "Projects");
        }
    }
}
