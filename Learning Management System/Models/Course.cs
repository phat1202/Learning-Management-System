using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class Course
    {
        [Key]
        public int? CourseId { get; set; }
        [Required]
        public string? CourseTitle { get; set; } // HTML, Python, C#, Tài Xỉu, Poker, Blackjack
        [Required]
        public string? CourseDescription { get; set; }
        public string? Id { get; set; }

        [ForeignKey(nameof(ApplicationUser.Id))]
        [Required]
        public ApplicationUser? Teacher { get; set; }
        public int? CategoryId { get; set; } //Front-End, Back-end, Finance
        [ForeignKey("CategoryId")]
        [Required]
        public CategoryCourse? Category { get; set; }
        public DateTime? DateStarted { get; set; } // Start when registered
        public DateTime? DateCompleted { get; set; }
        

       
    }
    public class Chapter
    {
        [Key]
        public int? ChapterId { get; set; }
        public int? ChapterNumber { get; set; }
        public string? ChapterTitle { get; set; }
        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        [Required]
        public Course? course { get; set; }

        public int? TotalNumberOfLesson { get; set; }
    }
    public class Lesson
    {
        [Key]
        public int? LessonId { get; set; }
        public string? LessonName { get; set; }
        [ForeignKey(nameof(Chapter.ChapterId))]
        public int? ChapterId { get; set; }

        public int? CourseId { get; set; }
        [ForeignKey("CourseId")]
        [Required]
        public Course? course { get; set; }
        [DefaultValue(false)]
        public bool IsCompleted { get; set; }
    }
}
