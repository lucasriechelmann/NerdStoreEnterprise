using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models;
public class UserLoginViewModel
{
    [Required(ErrorMessage = "The field {0} is mandatory")]
    [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "The field {0} is mandatory")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
}
