using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RPGAmbientApp.Migrations
{
    public partial class addEmailsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email1 = table.Column<string>(nullable: true),
                    Email2 = table.Column<string>(nullable: false),
                    Email3 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Email");
        }
    }
}
