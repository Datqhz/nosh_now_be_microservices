using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OrderService.Data.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class SetupAndModifySomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_Category_CategoryId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                schema: "order",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Restaurant_RestaurantId",
                schema: "order",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Food_FoodId",
                schema: "order",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                schema: "order",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredIngredient_Food_FoodId",
                schema: "order",
                table: "RequiredIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredIngredient_Ingredient_IngredientId",
                schema: "order",
                table: "RequiredIngredient");

            migrationBuilder.AddColumn<int>(
                name: "BoomCount",
                schema: "order",
                table: "Customer",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "NoshPoint",
                schema: "order",
                table: "Customer",
                type: "decimal",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Voucher",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    VoucherName = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Expired = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoucherWallet",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherWallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherWallet_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "order",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoshPointTransaction",
                schema: "order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "decimal", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TransactionType = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    VoucherId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoshPointTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoshPointTransaction_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "order",
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NoshPointTransaction_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalSchema: "order",
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoshPointTransaction_CustomerId",
                schema: "order",
                table: "NoshPointTransaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_NoshPointTransaction_VoucherId",
                schema: "order",
                table: "NoshPointTransaction",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherWallet_CustomerId",
                schema: "order",
                table: "VoucherWallet",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Category_CategoryId",
                schema: "order",
                table: "Food",
                column: "CategoryId",
                principalSchema: "order",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Food_Restaurant_RestaurantId",
                schema: "order",
                table: "Food",
                column: "RestaurantId",
                principalSchema: "order",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Restaurant_RestaurantId",
                schema: "order",
                table: "Ingredient",
                column: "RestaurantId",
                principalSchema: "order",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "order",
                table: "Order",
                column: "CustomerId",
                principalSchema: "order",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                schema: "order",
                table: "Order",
                column: "PaymentMethodId",
                principalSchema: "order",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                schema: "order",
                table: "Order",
                column: "RestaurantId",
                principalSchema: "order",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                schema: "order",
                table: "Order",
                column: "ShipperId",
                principalSchema: "order",
                principalTable: "Shipper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Food_FoodId",
                schema: "order",
                table: "OrderDetail",
                column: "FoodId",
                principalSchema: "order",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                schema: "order",
                table: "OrderDetail",
                column: "OrderId",
                principalSchema: "order",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredIngredient_Food_FoodId",
                schema: "order",
                table: "RequiredIngredient",
                column: "FoodId",
                principalSchema: "order",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredIngredient_Ingredient_IngredientId",
                schema: "order",
                table: "RequiredIngredient",
                column: "IngredientId",
                principalSchema: "order",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Restaurant_RestaurantId",
                schema: "order",
                table: "Ingredient");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                schema: "order",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Food_FoodId",
                schema: "order",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                schema: "order",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredIngredient_Food_FoodId",
                schema: "order",
                table: "RequiredIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_RequiredIngredient_Ingredient_IngredientId",
                schema: "order",
                table: "RequiredIngredient");

            migrationBuilder.DropTable(
                name: "NoshPointTransaction",
                schema: "order");

            migrationBuilder.DropTable(
                name: "VoucherWallet",
                schema: "order");

            migrationBuilder.DropTable(
                name: "Voucher",
                schema: "order");

            migrationBuilder.DropColumn(
                name: "BoomCount",
                schema: "order",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "NoshPoint",
                schema: "order",
                table: "Customer");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Restaurant_RestaurantId",
                schema: "order",
                table: "Ingredient",
                column: "RestaurantId",
                principalSchema: "order",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Customer_CustomerId",
                schema: "order",
                table: "Order",
                column: "CustomerId",
                principalSchema: "order",
                principalTable: "Customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentMethod_PaymentMethodId",
                schema: "order",
                table: "Order",
                column: "PaymentMethodId",
                principalSchema: "order",
                principalTable: "PaymentMethod",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                schema: "order",
                table: "Order",
                column: "RestaurantId",
                principalSchema: "order",
                principalTable: "Restaurant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Shipper_ShipperId",
                schema: "order",
                table: "Order",
                column: "ShipperId",
                principalSchema: "order",
                principalTable: "Shipper",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Food_FoodId",
                schema: "order",
                table: "OrderDetail",
                column: "FoodId",
                principalSchema: "order",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                schema: "order",
                table: "OrderDetail",
                column: "OrderId",
                principalSchema: "order",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredIngredient_Food_FoodId",
                schema: "order",
                table: "RequiredIngredient",
                column: "FoodId",
                principalSchema: "order",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequiredIngredient_Ingredient_IngredientId",
                schema: "order",
                table: "RequiredIngredient",
                column: "IngredientId",
                principalSchema: "order",
                principalTable: "Ingredient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
