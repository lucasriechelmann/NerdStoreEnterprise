using NSE.WebApp.MVC.Extensions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NSE.WebApp.MVC.Models;
public class UserRegisterViewModel
{
    [Required(ErrorMessage = "The field {0} is mandatory")]
    [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "The field {0} is mandatory")]
    [StringLength(100, ErrorMessage = "The field {0} must have between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public string PasswordConfirm { get; set; }
    [Required(ErrorMessage = "The field {0} is mandatory")]
    [DisplayName("CPF")]
    [Cpf]
    public string Cpf { get; set; }
    [Required(ErrorMessage = "The field {0} is mandatory")]
    [DisplayName("Full Name")]
    public string Name { get; set; }
}

public class UserRegister
{
    [Required(ErrorMessage = "The field {0} is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    public string Cpf { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The field {0} is in invalid format")]
    public string Email { get; set; }
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "The passwords do not match")]
    public string PasswordConfirm { get; set; }
}
