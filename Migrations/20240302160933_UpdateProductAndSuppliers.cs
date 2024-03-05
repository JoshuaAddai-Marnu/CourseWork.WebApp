using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopSite.CW.WebApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductAndSuppliers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "ProductSupplier");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "products",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsShipped",
                table: "Orders",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "INTEGER");

            migrationBuilder.CreateIndex(
                name: "IX_products_CategoryId",
                table: "products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_products_SupplierId",
                table: "products",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_Categories_CategoryId",
                table: "products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_products_suppliers_SupplierId",
                table: "products",
                column: "SupplierId",
                principalTable: "suppliers",
                principalColumn: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_Categories_CategoryId",
                table: "products");

            migrationBuilder.DropForeignKey(
                name: "FK_products_suppliers_SupplierId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_CategoryId",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SupplierId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "products");

            migrationBuilder.AlterColumn<bool>(
                name: "IsShipped",
                table: "Orders",
                type: "INTEGER",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsProductId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesCategoryId, x.ProductsProductId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSupplier",
                columns: table => new
                {
                    ProductsProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    SuppliersSupplierId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSupplier", x => new { x.ProductsProductId, x.SuppliersSupplierId });
                    table.ForeignKey(
                        name: "FK_ProductSupplier_products_ProductsProductId",
                        column: x => x.ProductsProductId,
                        principalTable: "products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSupplier_suppliers_SuppliersSupplierId",
                        column: x => x.SuppliersSupplierId,
                        principalTable: "suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsProductId",
                table: "CategoryProduct",
                column: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSupplier_SuppliersSupplierId",
                table: "ProductSupplier",
                column: "SuppliersSupplierId");
        }
    }
}
