using Microsoft.EntityFrameworkCore.Migrations;

namespace stockDataEF.Migrations
{
    public partial class avaliablestocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Stocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ApplicationUserId",
                table: "Stocks",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_AspNetUsers_ApplicationUserId",
                table: "Stocks",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_AspNetUsers_ApplicationUserId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ApplicationUserId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Stocks");
        }
    }
}
