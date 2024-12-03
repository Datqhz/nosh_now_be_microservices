using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class addcolumninshipper : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFree",
                schema: "order",
                table: "Shipper",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFree",
                schema: "order",
                table: "Shipper");
        }
    }
}
