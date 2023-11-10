using System.ComponentModel.DataAnnotations;

namespace Learning_Management_System.Repositories.ModelRepositories
{
    public class RegisterModel
    {
        [Required]
        public string? UserName { get; set; }
        public enum Gender
        {
            Male,
            Female,
            Unknow,
        } 

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]

        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
