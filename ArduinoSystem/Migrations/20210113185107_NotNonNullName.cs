using Microsoft.EntityFrameworkCore.Migrations;

namespace ArduinoSystem.Migrations
{
    public partial class NotNonNullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                table: "AspNetUsers", 
                name: "Name",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                table: "AspNetUsers",
                name: "Name",
                nullable: true);
        }
    }
}
