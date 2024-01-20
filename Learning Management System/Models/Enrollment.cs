using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class Enrollment
    {
        [Key]
        public int? EnrollmentID { get; set; }
        public string? UserId { get; set; }
        public int? CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; } = DateTime.Now;
        [ForeignKey(nameof(UserId))]
        [Required]
        public User? user { get; set; }
        [ForeignKey(nameof(CourseId))]
        [Required]
        public Course? course { get; set; }
    }
}
