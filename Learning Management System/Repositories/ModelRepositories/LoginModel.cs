using System.ComponentModel.DataAnnotations;

namespace Learning_Management_System.Repositories.ModelRepositories
{
    public class LoginModel
    {
        [Required, EmailAddress]
        [Display(Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        public string? Password { get; set; }
    }
}
