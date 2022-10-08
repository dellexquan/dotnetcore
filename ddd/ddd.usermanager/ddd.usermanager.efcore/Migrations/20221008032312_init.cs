using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ddd.usermanager.efcore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLoginHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: true),
                    PhoneNumber_RegionCode = table.Column<int>(type: "INTEGER", unicode: false, maxLength: 5, nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLoginHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PhoneNumber_RegionCode = table.Column<int>(type: "INTEGER", unicode: false, maxLength: 5, nullable: false),
                    PhoneNumber_Number = table.Column<string>(type: "TEXT", unicode: false, maxLength: 20, nullable: false),
                    passwordHash = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessFail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    LockoutEndDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    isLockout = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessFail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAccessFail_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessFail_UserId",
                table: "UserAccessFail",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAccessFail");

            migrationBuilder.DropTable(
                name: "UserLoginHistories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
