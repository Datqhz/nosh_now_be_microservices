using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CoreService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class ModifySomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "core",
                table: "Restaurant");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "core",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "core",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "core",
                table: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                schema: "core",
                table: "Restaurant",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                schema: "core",
                table: "Employee",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                schema: "core",
                table: "Customer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                schema: "core",
                table: "Admin",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
