using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManager.Migrations
{
    public partial class AddedDiscontinuedToItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Discontinued",
                table: "Items",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discontinued",
                table: "Items");
        }
    }
}
