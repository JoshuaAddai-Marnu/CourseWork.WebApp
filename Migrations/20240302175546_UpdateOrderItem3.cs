﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSite.CW.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderItem3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_userId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Orders",
                newName: "CustomerId1");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userId",
                table: "Orders",
                newName: "IX_Orders_CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId1",
                table: "Orders",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CustomerId1",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "CustomerId1",
                table: "Orders",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId1",
                table: "Orders",
                newName: "IX_Orders_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_userId",
                table: "Orders",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
