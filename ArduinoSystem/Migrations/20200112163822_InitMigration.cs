using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ArduinoSystem.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 140, nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    Field1_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field2_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field3_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field4_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field5_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field6_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field7_Name = table.Column<string>(maxLength: 140, nullable: true),
                    Field8_Name = table.Column<string>(maxLength: 140, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Channels_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ChannelId = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Field1 = table.Column<double>(nullable: true),
                    Field2 = table.Column<double>(nullable: true),
                    Field3 = table.Column<double>(nullable: true),
                    Field4 = table.Column<double>(nullable: true),
                    Field5 = table.Column<double>(nullable: true),
                    Field6 = table.Column<double>(nullable: true),
                    Field7 = table.Column<double>(nullable: true),
                    Field8 = table.Column<double>(nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    Elevation = table.Column<double>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Channels_AccountId",
                table: "Channels",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_ChannelId",
                table: "Entries",
                column: "ChannelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
