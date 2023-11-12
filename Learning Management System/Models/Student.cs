using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class Student : BaseInfo
    {
        public string? Id { get; set; }

        [ForeignKey(nameof(ApplicationUser.Id))]
        [Required]
        public ApplicationUser? Learner { get; set; }
        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        [Required]
        public Course? course { get; set; }
    }
}
