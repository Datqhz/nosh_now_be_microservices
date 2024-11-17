using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class ModifySomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "order",
                table: "Food",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RestaurantId",
                schema: "order",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "order",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                schema: "order",
                table: "Employee");
        }
    }
}
