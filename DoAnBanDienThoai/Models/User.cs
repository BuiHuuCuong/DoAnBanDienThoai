using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnBanDienThoai.Models
{
    public class  User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(250)] 
        public string? UserName { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        [Required]
        public string? UserPassword { get; set; }
        public string? UserRole { get; set; }
        public ICollection<Contact>? Contact { get; set; }
        public ICollection<Order>? Order { get; set; }

    }
}
