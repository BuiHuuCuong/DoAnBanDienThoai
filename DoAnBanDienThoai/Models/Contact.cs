using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DoAnBanDienThoai.Models
{
    public class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [ForeignKey("UserID")]
        public int UserID { get; set; }

        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? UserEmail { get; set; }
        
        public string? UserPhone { get; set; }
        [Required]
        public string? Content { get; set; }
        
    }
}
