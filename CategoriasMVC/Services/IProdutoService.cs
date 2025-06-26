using CategoriasMVC.Models;

namespace CategoriasMVC.Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
    Task<ProdutoViewModel> GetProdutoPorId(int id, string token);
    Task<ProdutoViewModel> CriarProduto(ProdutoViewModel produto, string token);
    Task<bool> AtualizaProduto(int id, ProdutoViewModel produto, string token);
    Task<bool> DeletaProduto(int id, string token);
}
