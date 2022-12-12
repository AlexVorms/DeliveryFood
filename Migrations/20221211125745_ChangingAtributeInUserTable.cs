using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class ChangingAtributeInUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
