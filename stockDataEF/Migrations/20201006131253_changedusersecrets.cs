using Microsoft.EntityFrameworkCore.Migrations;

namespace stockDataEF.Migrations
{
    public partial class changedusersecrets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLogin_AspNetUsers_ApplicationUserId",
                table: "ExternalLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLogin_Stocks_StockId",
                table: "ExternalLogin");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalLogin",
                table: "ExternalLogin");

            migrationBuilder.RenameTable(
                name: "ExternalLogin",
                newName: "ExternalLogins");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalLogin_StockId",
                table: "ExternalLogins",
                newName: "IX_ExternalLogins_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalLogin_ApplicationUserId",
                table: "ExternalLogins",
                newName: "IX_ExternalLogins_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalLogins",
                table: "ExternalLogins",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLogins_AspNetUsers_ApplicationUserId",
                table: "ExternalLogins",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLogins_Stocks_StockId",
                table: "ExternalLogins",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLogins_AspNetUsers_ApplicationUserId",
                table: "ExternalLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLogins_Stocks_StockId",
                table: "ExternalLogins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalLogins",
                table: "ExternalLogins");

            migrationBuilder.RenameTable(
                name: "ExternalLogins",
                newName: "ExternalLogin");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalLogins_StockId",
                table: "ExternalLogin",
                newName: "IX_ExternalLogin_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalLogins_ApplicationUserId",
                table: "ExternalLogin",
                newName: "IX_ExternalLogin_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalLogin",
                table: "ExternalLogin",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLogin_AspNetUsers_ApplicationUserId",
                table: "ExternalLogin",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLogin_Stocks_StockId",
                table: "ExternalLogin",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
