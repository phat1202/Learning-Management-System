using Microsoft.AspNetCore.Identity;

namespace Learning_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsTeacher { get; set; }
        public enum Gender
        {
            Male,
            Female,
            Unknow,
        }
    }
}
