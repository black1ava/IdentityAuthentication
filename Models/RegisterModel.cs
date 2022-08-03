using System.ComponentModel.DataAnnotations;

namespace IdentityAuthentication.Models
{
  public class RegisterModel
  {
    public String Username { get; set; } = String.Empty;
    [EmailAddress]
    public String Email { get; set; } = String.Empty;
    [DataType(DataType.Password)]
    public String Password { get; set; } = String.Empty;
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password does not matched")]
    [Display(Name = "Password Confirmation")]
    public String PasswordConfirmation { get; set; } = String.Empty;
  }
}