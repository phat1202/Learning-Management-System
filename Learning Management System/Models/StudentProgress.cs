using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class StudentProgress
    {
        [ForeignKey(nameof(Student.StudentId))]
        public string? StudentId { get; set; }
        [ForeignKey(nameof(Course.CourseId))]
        public int? CourseId { get; set; }
        public int? TotalActivity { get; set; }
        public int? TotalLessonIsCompleted { get; set; } // Check in Lesson.IsCompleted == True => Get (+1)
        public double? ProgressPercentage { get; set; } //  (TotalLesson / TotalActivity) * 100 =>   x%
    }
}
