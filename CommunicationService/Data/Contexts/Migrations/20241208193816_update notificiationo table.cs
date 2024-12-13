using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommunicationService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class updatenotificiationotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotifyType",
                schema: "core",
                table: "Notification");

            migrationBuilder.AddColumn<int>(
                name: "ReceiverType",
                schema: "core",
                table: "Notification",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverType",
                schema: "core",
                table: "Notification");

            migrationBuilder.AddColumn<string>(
                name: "NotifyType",
                schema: "core",
                table: "Notification",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
