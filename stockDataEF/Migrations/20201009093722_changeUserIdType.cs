﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace stockDataEF.Migrations
{
    public partial class changeUserIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLogins_AspNetUsers_ApplicationUserId1",
                table: "ExternalLogins");

            migrationBuilder.DropIndex(
                name: "IX_ExternalLogins_ApplicationUserId1",
                table: "ExternalLogins");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId1",
                table: "ExternalLogins");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "ExternalLogins",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLogins_ApplicationUserId",
                table: "ExternalLogins",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLogins_AspNetUsers_ApplicationUserId",
                table: "ExternalLogins",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalLogins_AspNetUsers_ApplicationUserId",
                table: "ExternalLogins");

            migrationBuilder.DropIndex(
                name: "IX_ExternalLogins_ApplicationUserId",
                table: "ExternalLogins");

            migrationBuilder.AlterColumn<int>(
                name: "ApplicationUserId",
                table: "ExternalLogins",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId1",
                table: "ExternalLogins",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLogins_ApplicationUserId1",
                table: "ExternalLogins",
                column: "ApplicationUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalLogins_AspNetUsers_ApplicationUserId1",
                table: "ExternalLogins",
                column: "ApplicationUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
