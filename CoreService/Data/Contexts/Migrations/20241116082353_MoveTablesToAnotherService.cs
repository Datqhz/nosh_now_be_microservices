using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class MoveTablesToAnotherService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequiredIngredient",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Food",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Ingredient",
                schema: "core");

            migrationBuilder.DropTable(
                name: "Category",
                schema: "core");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredient",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RestaurantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 200, nullable: false),
                    Quantity = table.Column<double>(type: "double precision", nullable: false, defaultValue: 0.0),
                    Unit = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredient_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalSchema: "core",
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                schema: "core",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "varchar", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "core",
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Restaurant_RestaurantId",
                        column: x => x.RestaurantId,
                        principalSchema: "core",
                        principalTable: "Restaurant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequiredIngredient",
                schema: "core",
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
                        principalSchema: "core",
                        principalTable: "Food",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequiredIngredient_Ingredient_IngredientId",
                        column: x => x.IngredientId,
                        principalSchema: "core",
                        principalTable: "Ingredient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_CategoryId",
                schema: "core",
                table: "Food",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_RestaurantId",
                schema: "core",
                table: "Food",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_RestaurantId",
                schema: "core",
                table: "Ingredient",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredIngredient_FoodId",
                schema: "core",
                table: "RequiredIngredient",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_RequiredIngredient_IngredientId",
                schema: "core",
                table: "RequiredIngredient",
                column: "IngredientId");
        }
    }
}
