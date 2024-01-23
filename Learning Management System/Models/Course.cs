using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

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
        public string? TeacherId { get; set; }
        public decimal? Price { get; set; }
        public string? ImageCover { get; set; }
        public List<Rating>? Rating { get; set; }

        [ForeignKey(nameof(TeacherId))]
        [Required]
        public User? Teacher { get; set; }
        public int? CategoryId { get; set; } //Front-End, Back-end, Finance
        [ForeignKey("CategoryId")]
        [Required]
        public CategoryCourse? Category { get; set; }        
    }
    public class Chapter
    {
        [Key]
        public int? ChapterId { get; set; }
        public string? ChapterTitle { get; set; }
        public int? ChapterNumber {  get; set; }
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
        public int? LessonNumber { get; set; }
        public int? ChapterId { get; set; }
        public string? ContentUrl { get; set; }
        [ForeignKey(nameof(Chapter.ChapterId))]
        public Chapter? chapter { get; set; }
        public double? TimeDuration { get; set; }

    }
}
