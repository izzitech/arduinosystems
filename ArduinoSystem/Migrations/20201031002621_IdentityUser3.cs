using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ArduinoSystem.Migrations
{
    public partial class IdentityUser3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_Accounts_AccountId",
                table: "Channels");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Channels_AccountId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Channels");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Channels",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Channels_UserId",
                table: "Channels",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_AspNetUsers_UserId",
                table: "Channels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Channels_AspNetUsers_UserId",
                table: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Channels_UserId",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Channels");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Channels",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 140);

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(140)", maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_AccountId",
                table: "Channels",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Channels_Accounts_AccountId",
                table: "Channels",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
