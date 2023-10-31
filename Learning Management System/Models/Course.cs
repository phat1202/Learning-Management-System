using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string? CourseTitle { get; set; } // HTML, Python, C#, Tài Xỉu, Poker, Blackjack
        public string? CourseDescription { get; set; }
        [ForeignKey(nameof(Teacher.TeacherId))]
        public string? TeacherId { get; set; } // If UserType == Teacher => Get UserId => Teacher Quoc Name
        [ForeignKey(nameof(CategoryCourse.CategoryId))]
        public int? CategoryId { get; set; } //Front-End, Back-end, Finance 
        public DateTime? DateStarted { get; set; } // Start when registered
        public DateTime? DateExpired { get; set; } // Ex: 2 months
        public DateTime? DateCompleted { get; set; }
       
    }
    public class Chapter
    {
        [Key]
        public int? ChapterId { get; set; }
        public int? ChapterNumber { get; set; }
        public string? ChapterTitle { get; set; }
        [ForeignKey(nameof(Course.CourseId))]
        public int? CourseId { get; set; }
        public int? TotalNumberOfLesson { get; set; }
    }
    public class Lesson
    {
        [Key]
        public int? LessonId { get; set; }
        public string? LessonName { get; set; }
        [ForeignKey(nameof(Chapter.ChapterId))]
        public int? ChapterId { get; set; }
        [ForeignKey(nameof(Course.CourseId))]
        public int? CourseId { get; set; }
        [DefaultValue(false)]
        public bool IsCompleted { get; set; }
    }
}
