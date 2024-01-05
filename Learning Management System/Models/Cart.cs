using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Learning_Management_System.Models
{
    public class Cart
    {
        public Cart()
        {
            CartId = Guid.NewGuid().ToString();
        }
        [Key]
        public string? CartId { get; set; }
        public decimal? TotalPrice { get; set; }
    }
    public class CartItem
    {
        [Key]
        public int? CartItemId { get; set; }
        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? course { get; set; }
        public int? Quantity { get; set; }
        public string? CartId { get; set; }
        [ForeignKey("CartId")]
        [Required]
        public Cart? cart { get; set; }
    }
}
