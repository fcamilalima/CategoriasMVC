using System.ComponentModel.DataAnnotations;

namespace CategoriasMVC.Models;

public class CategoriaViewModel
{
    public int CategoriaID { get; set; }

    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    public string? Nome { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Imagem")]
    public string? imageURL { get; set; }
}
