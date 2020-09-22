using Microsoft.EntityFrameworkCore.Migrations;

namespace NilDevStudio.WebAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MyEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Local = table.Column<string>(nullable: true),
                    DataEvent = table.Column<string>(nullable: true),
                    theme = table.Column<string>(nullable: true),
                    QuantPeople = table.Column<int>(nullable: false),
                    Lot = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEvents", x => x.EventId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyEvents");
        }
    }
}
