using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Media_Sharing_Site.Migrations
{
    public partial class MediaStocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stocks",
                table: "Medias",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stocks",
                table: "Medias");
        }
    }
}
