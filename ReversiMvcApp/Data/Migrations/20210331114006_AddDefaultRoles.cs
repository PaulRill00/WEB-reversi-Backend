using Microsoft.EntityFrameworkCore.Migrations;

namespace ReversiMvcApp.Data.Migrations
{
    public partial class AddDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new [] {"Id", "Name","NormalizedName"},
                values: new object[,]
                {
                    { "1", "User", "USER" },
                    { "2", "Mediator", "MEDIATOR" },
                    { "3", "Admin", "ADMIN" }
                }
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
