using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnBanDienThoai.Migrations
{
    public partial class InitialOracle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    BrandId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    BrandName = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandId);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProductName = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Quantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Price = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    Image = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItemId);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CategoryName = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserName = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: false),
                    UserEmail = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserPassword = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    UserRole = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ProductName = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: false),
                    ProductDescription = table.Column<string>(type: "NVARCHAR2(500)", maxLength: 500, nullable: true),
                    ProductQuantity = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProductImage = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    CategoryId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BrandId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Product_Brand_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brand",
                        principalColumn: "BrandId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    UserPhone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Content = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contact_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    UserID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Address = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Phone = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Total = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItemOrder",
                columns: table => new
                {
                    CartItemsCartItemId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    OrderID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemOrder", x => new { x.CartItemsCartItemId, x.OrderID });
                    table.ForeignKey(
                        name: "FK_CartItemOrder_CartItem_CartItemsCartItemId",
                        column: x => x.CartItemsCartItemId,
                        principalTable: "CartItem",
                        principalColumn: "CartItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItemOrder_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemOrder_OrderID",
                table: "CartItemOrder",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_UserID",
                table: "Contact",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserID",
                table: "Order",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_BrandId",
                table: "Product",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CategoryId",
                table: "Product",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemOrder");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
