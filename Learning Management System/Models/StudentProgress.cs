using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class StudentProgress
    {
        [Key]
        public int? ProgressId { get; set; }

        [Required]
        public string? UserId { get; set; }

        [Required]
        public int? LessonId { get; set; }

        public string? CompletionStatus { get; set; }

        public int? Score { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LastAccessed { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? user { get; set; }
        [ForeignKey(nameof(LessonId))]
        public Lesson? lesson { get; set; }
    }
}
