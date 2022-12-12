using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class ChangingAtributeInUserTable4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishEntityUserEntity");

            migrationBuilder.AddColumn<Guid>(
                name: "UserEntityId",
                table: "Dish",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dish_UserEntityId",
                table: "Dish",
                column: "UserEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dish_User_UserEntityId",
                table: "Dish",
                column: "UserEntityId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dish_User_UserEntityId",
                table: "Dish");

            migrationBuilder.DropIndex(
                name: "IX_Dish_UserEntityId",
                table: "Dish");

            migrationBuilder.DropColumn(
                name: "UserEntityId",
                table: "Dish");

            migrationBuilder.CreateTable(
                name: "DishEntityUserEntity",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishEntityUserEntity", x => new { x.BasketId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_DishEntityUserEntity_Dish_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Dish",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishEntityUserEntity_User_UsersId",
                        column: x => x.UsersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishEntityUserEntity_UsersId",
                table: "DishEntityUserEntity",
                column: "UsersId");
        }
    }
}
