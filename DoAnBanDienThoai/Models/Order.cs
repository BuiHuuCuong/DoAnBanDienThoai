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
        [Required]
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
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
            CartItem newCartItem = new CartItem
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                Price = product.ProductPrice,
                Quantity = 1,
                Image = product.ProductImage
            };
            CartItems.Add(newCartItem);
        }
    }
}
