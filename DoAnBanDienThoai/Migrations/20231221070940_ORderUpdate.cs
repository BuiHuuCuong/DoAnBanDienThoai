using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoAnBanDienThoai.Migrations
{
    public partial class ORderUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_CartItem_CartItemId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_CartItemId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "CartItemId",
                table: "Order");

            migrationBuilder.CreateTable(
                name: "CartItemOrder",
                columns: table => new
                {
                    CartItemsCartItemId = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemOrder");

            migrationBuilder.AddColumn<int>(
                name: "CartItemId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_CartItemId",
                table: "Order",
                column: "CartItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_CartItem_CartItemId",
                table: "Order",
                column: "CartItemId",
                principalTable: "CartItem",
                principalColumn: "CartItemId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
