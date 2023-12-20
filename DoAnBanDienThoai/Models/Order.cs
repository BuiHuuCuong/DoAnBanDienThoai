using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnBanDienThoai.Models
{
    public class Order
    {

        [Key]
        public int OrderID { get; set; }

        [ForeignKey("UserID")]
        public int UserID { get; set; }
        public User? User { get; set; }

        [ForeignKey("CartItemId")]
        public int CartItemId { get; set; }
        public CartItem? CartItem { get; set; }
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Phone { get; set; }
        public decimal? Total { get; set; }
        public DateTime? OrderDate { get; set; }

        public Order()
        {

        }

        public Order(Product product)
        {
            CartItem.ProductId = product.ProductId;
            CartItem.ProductName = product.ProductName;
            CartItem.Price = product.ProductPrice;
            CartItem.Quantity = 1;
            CartItem.Image = product.ProductImage;
        }
    }
}
