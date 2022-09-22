using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ddd.demo1.Migrations
{
    public partial class blog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Title_Chinese = table.Column<string>(type: "TEXT", nullable: true),
                    Title_English = table.Column<string>(type: "TEXT", nullable: true),
                    Body_Chinese = table.Column<string>(type: "TEXT", nullable: true),
                    Body_English = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
