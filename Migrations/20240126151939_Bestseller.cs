using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Books.Migrations
{
    public partial class Bestseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BestSellers",
                table: "BestSellers");

            migrationBuilder.RenameTable(
                name: "BestSellers",
                newName: "Bestseller");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bestseller",
                table: "Bestseller",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Bestseller",
                table: "Bestseller");

            migrationBuilder.RenameTable(
                name: "Bestseller",
                newName: "BestSellers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BestSellers",
                table: "BestSellers",
                column: "Id");
        }
    }
}
