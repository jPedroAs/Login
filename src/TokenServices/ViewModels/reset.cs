using System.ComponentModel.DataAnnotations;

namespace Login.ViewModels;

public class ReseViewModel
{
    public ReseViewModel()
    {
        Email = string.Empty;
    }

    [Required(ErrorMessage = "Obrigatório")]
    public string Email {get; set;}

}