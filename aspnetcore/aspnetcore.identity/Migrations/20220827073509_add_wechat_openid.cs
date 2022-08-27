using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnetcore.identity.Migrations
{
    public partial class add_wechat_openid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WechatOpenId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WechatOpenId",
                table: "AspNetUsers");
        }
    }
}
