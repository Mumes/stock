using Microsoft.EntityFrameworkCore.Migrations;

namespace stockDataEF.Migrations
{
    public partial class new_migr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "ExternalLogin",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    StockId = table.Column<int>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalLogin_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExternalLogin_Stocks_StockId",
                        column: x => x.StockId,
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLogin_ApplicationUserId",
                table: "ExternalLogin",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLogin_StockId",
                table: "ExternalLogin",
                column: "StockId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalLogin");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Stocks",
                type: "nvarchar(450)",
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
    }
}
