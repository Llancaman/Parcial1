using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Parcial1.Models;
using Parcial1.Views.Roles.ViewModels;

namespace Parcial1.Controllers;

[Authorize(Roles = "Administrador")]
public class RolesController : Controller
{
    private readonly ILogger<HomeController> _logger;
    //Inyectamos el role manager
    private readonly RoleManager<IdentityRole> _roleManager;

    //Añadimos el userManager
    public RolesController(
        ILogger<HomeController> logger,
        RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        //Listar todos los roles.
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(RoleCreateViewModel model)
    {
        if(string.IsNullOrEmpty(model.RoleName))
        {
            return View();
        }
        var role = new IdentityRole(model.RoleName);
        _roleManager.CreateAsync(role);

        return RedirectToAction("Index");
    }
}