using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryManager.Migrations
{
    public partial class AddedFieldsItemAndSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SKUValue",
                table: "Items");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Suppliers",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SKUTotalValue",
                table: "Items",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Suppliers");

            migrationBuilder.DropColumn(
                name: "SKUTotalValue",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "SKUValue",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
