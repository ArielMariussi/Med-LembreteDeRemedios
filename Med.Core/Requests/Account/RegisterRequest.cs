using System.ComponentModel.DataAnnotations;

namespace Med.Core.Requests.Account;

public class RegisterRequest : Request
{
    [Required(ErrorMessage = "E-mail")]
    [EmailAddress(ErrorMessage = "E-mail invalido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Senha Invalida")]
    public string Password { get; set; } = string.Empty;
}
