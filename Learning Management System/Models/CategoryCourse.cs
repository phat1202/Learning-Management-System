using System.ComponentModel.DataAnnotations;

namespace Learning_Management_System.Models
{
    public class CategoryCourse
    {
        [Key]
        public int? CategoryId { get; set; }
        [Required]
        public string? CategoryName { get; set; } //Back-end, Finance,...
        public string? CategoryImageCover { get; set; }
    }
}
