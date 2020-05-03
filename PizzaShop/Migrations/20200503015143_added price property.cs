using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaShop.Migrations
{
    public partial class addedpriceproperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "OrderDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetails");
        }
    }
}
