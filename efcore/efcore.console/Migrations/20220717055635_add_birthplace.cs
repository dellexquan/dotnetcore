using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcore.console.Migrations
{
    public partial class add_birthplace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "Persons",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "Persons");
        }
    }
}
