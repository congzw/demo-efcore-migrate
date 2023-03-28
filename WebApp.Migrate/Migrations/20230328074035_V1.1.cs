using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NbSites.Web.Migrations
{
    public partial class V11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "demo_course",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "demo_course",
                type: "TEXT",
                maxLength: 2000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "demo_course");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "demo_course",
                newName: "Title");
        }
    }
}
