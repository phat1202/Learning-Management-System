using System.ComponentModel.DataAnnotations;

namespace Learning_Management_System.Repositories.AccountRepository
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "*Username is required.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "*Password must be at least 8 characters.")]
        [Display(Name = "Password")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@$!%*?&]+$",
        ErrorMessage = "*Password must contain at least one number, and one special characters.")]

        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "*The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
        [Required]
        [Display(Name = "Your Gender")]
        public int? Gender { get; set; }
        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
