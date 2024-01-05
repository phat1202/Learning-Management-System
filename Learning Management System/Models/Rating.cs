using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [ForeignKey(nameof(CourseId))]
        public Course? course { get; set; }
        public int? CourseId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? user { get; set; }
        public string? UserId { get; set; }
        public int? StarRate { get; set; }
        [MaxLength(255)]
        public string? Comment { get; set; }
    }
}
