using System.ComponentModel.DataAnnotations;

namespace AspNetPbkdf2Authentication.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(4, ErrorMessage = "Username has to have atleast 4 characters.")]
        [MaxLength(50, ErrorMessage = "Username has to have maximum of 50 characters.")]
        [Display(Name = "Username", Description = "Username has to have atleast 4 and maximum of 50 characters")]
        public string Username { get; set; } = null!;
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password has to have atleast 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Description = "Password has to have atleast 8 characters.")]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password has to have atleast 8 characters.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmation Password", Description = "Confirmation password has to be same as password.")]
        public string ConfirmationPassword { get; set; } = null!;
    }
}
