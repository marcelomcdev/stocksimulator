using System.ComponentModel.DataAnnotations;

namespace StockSimulator.Application.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "The email is an invalid format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, ErrorMessage = "The field {0} must be between {2} e {1} characters.", MinimumLength = 8)]
        public string Password { get; set; }
    }
}
