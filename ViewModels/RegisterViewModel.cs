using System.ComponentModel.DataAnnotations;

namespace Parking.ViewModels;

public class RegisterViewModel
{
    public RegisterViewModel()
    {
        Name = string.Empty;
        Email = string.Empty;
        Birthday = string.Empty;
        Password = string.Empty;
    }
    [Required(ErrorMessage = "Obrigatório")]
    [MaxLength(255)]
    public string Name {get; set;}

    [Required(ErrorMessage = "Obrigatório")]
    [EmailAddress(ErrorMessage = "Necessário um email válido")]  
    public string Email {get; set;}

    [Required(ErrorMessage = "Obrigatório")]
    [MaxLength(255)]
    public string Birthday { get; set; }

    [Required(ErrorMessage = "Senha é Obrigatório e deve ter mais de 8 caracteres")]
    [StringLength(20, MinimumLength = 8)]
    public string Password { get; set; }
}