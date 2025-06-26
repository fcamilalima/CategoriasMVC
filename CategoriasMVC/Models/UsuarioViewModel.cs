using System.ComponentModel.DataAnnotations;

namespace CategoriasMVC.Models;

public class UsuarioViewModel
{
    [Display(Name = "Usuário")]
    public string? userName { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string? password { get; set; }
}
