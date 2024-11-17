using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OrderService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesFromCoreService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "order",
                table: "Food",
                type: "varchar",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "order",
                table: "Food",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "order",
                table: "Food",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "order",
                table: "Food",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RestaurantId",
                schema: "order",
                table: "Food",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Category",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    Unit = table.Column<int>(type: "integer", nullable: false),
                    RestaurantId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalSchema: "order",
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredIngredient",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FoodId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredIngredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequiredIngredient_Food_FoodId",
                        column: x => x.FoodId,
                        principalSchema: "order",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "order",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_CategoryId",
                schema: "order",
                table: "Food",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_RestaurantId",
                schema: "order",
                table: "Food",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RestaurantId",
                schema: "order",
                table: "Ingredient",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredIngredient_FoodId",
                schema: "order",
                table: "RequiredIngredient",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredIngredient_IngredientId",
                schema: "order",
                table: "RequiredIngredient",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Category_CategoryId",
                schema: "order",
                table: "Food",
                column: "CategoryId",
                principalSchema: "order",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                schema: "order",
                table: "Food",
                column: "RestaurantId",
                principalSchema: "order",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Category_CategoryId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "order");

            migrationBuilder.DropTable(
                name: "RequiredIngredient",
                schema: "order");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "order");

            migrationBuilder.DropIndex(
                name: "IX_Food_CategoryId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_RestaurantId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "order",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Price",
                schema: "order",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                schema: "order",
                table: "Food");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "order",
                table: "Food",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 100);
        }
    }
}
