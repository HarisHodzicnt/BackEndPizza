using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaShop.Migrations
{
    public partial class added2newpropertiestofoodtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aditional",
                table: "OrderDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Material",
                table: "Foods",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Foods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aditional",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "Material",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Foods");
        }
    }
}
