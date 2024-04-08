using System.ComponentModel.DataAnnotations;

namespace Parking.ViewModels;

public class LoginViewModel
{
    public LoginViewModel()
    {
        Email = string.Empty;
        Password = string.Empty;
    }

    [Required(ErrorMessage = "Obrigatório")]
    [EmailAddress(ErrorMessage = "Necessário um email válido")]  
    public string Email {get; set;}

    [Required(ErrorMessage = "Senha é Obrigatório e deve ter mais de 8 caracteres")]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; }
}