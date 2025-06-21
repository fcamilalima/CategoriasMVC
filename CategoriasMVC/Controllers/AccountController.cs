﻿using CategoriasMVC.Models;
using CategoriasMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAutenticacao _autenticacaoService;

    public AccountController(IAutenticacao autenticacaoService)
    {
        _autenticacaoService = autenticacaoService;
    }

    [HttpGet]
    public ActionResult Login()
    {
        return View();
    }

    public async Task<ActionResult> Login(UsuarioViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError(string.Empty, "Login inválido... ");
            return View(model);
        }

        var result = await _autenticacaoService.AutenticaUsuario(model);

        if(result is null)
        {
            ModelState.AddModelError(string.Empty, "Login inválido... ");
            return View(model);
        }

        Response.Cookies.Append("X-Access-Token", result.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });

        return RedirectToAction("/");
    }
}
