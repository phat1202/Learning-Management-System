using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning_Management_System.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

        [DefaultValue(false)]
        public bool IsTeacher { get; set; }
        public enum Gender
        {
            Male,
            Female,
            Unknow,
        }
    }
}
