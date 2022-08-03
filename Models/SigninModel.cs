using System.ComponentModel.DataAnnotations;

namespace IdentityAuthentication.Models
{
  public class SigninModel
  {
    [EmailAddress]
    public String Email { get; set; } = String.Empty;
    [DataType(DataType.Password)]
    public String Password { get; set; } = String.Empty;
  }
}