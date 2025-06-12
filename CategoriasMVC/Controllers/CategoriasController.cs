using CategoriasMVC.Models;
using CategoriasMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMVC.Controllers;

public class CategoriasController : Controller
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
    {
        var result = await _categoriaService.GetCategoriasAsync();

        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpGet]
    public IActionResult CriarNovaCategoria()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> CriarNovaCategoria(CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaService.CriarCategoriaAsync(categoriaVM);
            if (result is not null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        ViewBag.Erro = "Erro ao criar categoria.";
        return View(categoriaVM);
    }

    [HttpGet]
    public async Task<IActionResult> AtualizarCategoria(int id)
    {
        var result = await _categoriaService.GetCategoriaPorIdAsync(id);
        if (result is null)
        {
            return View("Error");
        }
        return View(result);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> AtualizarCategoria(int id,
        CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaService.AtualizarCategoriaAsync(id, categoriaVM);
            if (result)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        ViewBag.Erro = "Erro ao atualizar categoria.";
        return View(categoriaVM);
    }

    [HttpGet]
    public async Task<ActionResult> DeletarCategoria(int id)
    {
        var result = await _categoriaService.GetCategoriaPorIdAsync(id);

        if(result is null)
        {
            return View("Error");
        }

        return View(result);
    }

    [HttpPost(), ActionName("DeletarCategoria")]
    public async Task<ActionResult> DeletarConfirmado(int id)
    {
        var result = await _categoriaService.DeletaCategoriaAsync(id);
        if (result)
        {
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Erro = "Erro ao deletar categoria.";
        return View("Error");
    }
}