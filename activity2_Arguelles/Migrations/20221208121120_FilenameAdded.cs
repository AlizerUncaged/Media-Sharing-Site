using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media_Sharing_Site.Migrations
{
    public partial class FilenameAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Medias",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Medias");
        }
    }
}
