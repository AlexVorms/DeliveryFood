using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class NewTableBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishBasket");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketId",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BasketEntityId",
                table: "Dish",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Basket_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_BasketId",
                table: "Order",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Dish_BasketEntityId",
                table: "Dish",
                column: "BasketEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_Basket_BasketEntityId",
                table: "Dish",
                column: "BasketEntityId",
                principalTable: "Basket",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Basket_BasketId",
                table: "Order",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_Basket_BasketEntityId",
                table: "Dish");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Basket_BasketId",
                table: "Order");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Order_BasketId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Dish_BasketEntityId",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "BasketEntityId",
                table: "Dish");

            migrationBuilder.CreateTable(
                name: "DishBasket",
                columns: table => new
                {
                    DishId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DishId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishBasket", x => new { x.DishId, x.UserId });
                    table.ForeignKey(
                        name: "FK_DishBasket_Dish_DishId1",
                        column: x => x.DishId1,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishBasket_User_UserId1",
                        column: x => x.UserId1,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishBasket_DishId1",
                table: "DishBasket",
                column: "DishId1");

            migrationBuilder.CreateIndex(
                name: "IX_DishBasket_UserId1",
                table: "DishBasket",
                column: "UserId1");
        }
    }
}
