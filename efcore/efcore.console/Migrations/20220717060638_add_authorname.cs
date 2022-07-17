using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace efcore.console.Migrations
{
    public partial class add_authorname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Books",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Books");
        }
    }
}
