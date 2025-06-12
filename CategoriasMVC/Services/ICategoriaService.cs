using CategoriasMVC.Models;

namespace CategoriasMVC.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaViewModel>> GetCategoriasAsync();

    Task<CategoriaViewModel> GetCategoriaPorIdAsync(int id);

    Task<CategoriaViewModel> CriarCategoriaAsync(CategoriaViewModel categoriaVM);

    Task<bool> AtualizarCategoriaAsync(int id, CategoriaViewModel categoriaVM);

    Task<bool> DeletaCategoriaAsync(int id);
}
