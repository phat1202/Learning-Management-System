using System.ComponentModel.DataAnnotations;

namespace Learning_Management_System.Models
{
    public class EnumClass
    {
        public enum Gender
        {
            [Display(Name = "Male")]
            Male = 0,
            [Display(Name = "Female")]
            Female = 1,
            [Display(Name = "Other")]
            Other = 2,
        }
        public enum Role
        {
            [Display(Name = "Admin")]
            Admin = 0,
            [Display(Name = "Staff")]
            Staff = 1,
            [Display(Name = "Student")]
            Student = 2,
            [Display(Name = "Teacher")]
            Teacher = 3,
            [Display(Name = "User")]
            User = 4,
        }
        public enum RateStar
        {
            [Display(Name = "1")]
            Admin = 0,
            [Display(Name = "2")]
            Staff = 1,
            [Display(Name = "3")]
            Student = 2,
            [Display(Name = "4")]
            Teacher = 3,
            [Display(Name = "5")]
            User = 4,
        }
    }
}
