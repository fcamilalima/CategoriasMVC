using CategoriasMVC.Models;
using CategoriasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CategoriasMVC.Controllers;

public class ProdutosController : Controller
{
    private readonly IProdutoService _produtoService;
    private readonly ICategoriaService _categoriaService;
    private string token = String.Empty;
    public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
    {
        _produtoService = produtoService;
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
    {
        var result = await _produtoService.GetProdutos(ObtemTokenJwt());
        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpGet]
    public async Task<IActionResult> CriarNovoProduto()
    {
        ViewBag.CategoriasID = new SelectList(
            await _categoriaService.GetCategoriasAsync(), "CategoriaID", "Nome");
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> DetalhesProduto(int id)
    {
        var produto = await _produtoService.GetProdutoPorId(id, ObtemTokenJwt());

        if (produto is null)
            return View("Error");

        ViewBag.CategoriasID = new SelectList(
            await _categoriaService.GetCategoriasAsync(), "CategoriaID", "Nome");

        return View(produto);
    }

    [HttpGet]
    public async Task<IActionResult> AtualizarProduto(int id)
    {
        var produto = await _produtoService.GetProdutoPorId(id, ObtemTokenJwt());
        if (produto is null)
        {
            return View("Error");
        }
        ViewBag.CategoriasID = new SelectList(
            await _categoriaService.GetCategoriasAsync(), "CategoriaID", "Nome");
        return View(produto);
    }

    [HttpGet]
    public async Task<ActionResult> DeletarProduto(int id)
    {
        var produto = await _produtoService.GetProdutoPorId(id, ObtemTokenJwt());
        if (produto is null)
            return View("Error");

        return View(produto);
    }

    [HttpPost(), ActionName("DeletarProduto")]
    public async Task<IActionResult> DeletaConfirmado(int id)
    {
        var produto = await _produtoService.DeletaProduto(id, ObtemTokenJwt());
        if (produto)
            return RedirectToAction(nameof(Index));
        return View("Error");
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoViewModel>> AtualizarProduto(int id, ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.AtualizaProduto(id, produtoViewModel, ObtemTokenJwt());
            if (result)
                return RedirectToAction(nameof(Index));
        }
        return View(produtoViewModel);
    }
    [HttpPost]
    public async Task<ActionResult> CriarNovoProduto(ProdutoViewModel produtoViewModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _produtoService.CriarProduto(produtoViewModel, ObtemTokenJwt());
            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoriasId = new SelectList(
                    await _categoriaService.GetCategoriasAsync(), "CategoriaId", "Nome");
            }
        }
        return View(produtoViewModel);

    }

    private string ObtemTokenJwt()
    {
        if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
        {
            token = HttpContext.Request.Cookies["X-Access-Token"];
        }
        return token;
    }
}
