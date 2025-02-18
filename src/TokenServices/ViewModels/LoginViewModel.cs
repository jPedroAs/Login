using System.ComponentModel.DataAnnotations;

namespace Login.ViewModels;

public class LoginViewModel
{
    public LoginViewModel()
    {
        Sing = string.Empty;
        Password = string.Empty;
    }

    [Required(ErrorMessage = "Obrigatório")]
    public string Sing {get; set;}

    [Required(ErrorMessage = "Senha é Obrigatório e deve ter mais de 8 caracteres")]
    [StringLength(20, MinimumLength = 5)]
    public string Password { get; set; }
}