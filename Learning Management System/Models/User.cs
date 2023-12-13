using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace Learning_Management_System.Models
{
    public class User : BaseInfo
    {
        public User()
        {
            UserId = Guid.NewGuid().ToString();
        }
        [Key]
        public string? UserId { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Enter Email"), EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        [MaxLength(255, ErrorMessage = "Không được vượt quá 255 ký tự")]
        public string? Password { get; set; }
        public int? Role { get; set; }
        public int? Gender { get; set; }
        [DefaultValue(false)]
        public bool IsTeacher { get; set; }
        [DefaultValue(false)]
        public bool IsStudent { get; set; }
        public string? Avatar { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
