using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{

    public class Student : BaseInfo
    {
        [ForeignKey(nameof(ApplicationUser.Id))]
        public string? StudentId { get; set; }

        [ForeignKey(nameof(Course.CourseId))]
        public int? CourseId { get; set; }
    }
    public class Teacher : BaseInfo
    {
        [ForeignKey(nameof(ApplicationUser.Id))]
        public string? TeacherId { get; set; }

        [ForeignKey(nameof(Course.CourseId))]
        public int? CourseId { get; set; }
    }
}
