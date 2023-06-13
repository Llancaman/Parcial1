using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial1.Data;
using Parcial1.Models;
using Parcial1.Services;
using Parcial1.ViewModels;

namespace Parcial1.Controllers
{
    public class GeneroController : Controller
    {
        private readonly VideojuegoContext _context;
        private readonly IGeneroService _generoService;

        public GeneroController(VideojuegoContext context, IGeneroService generoService)
        {
            _context = context;
            _generoService = generoService;
        }

        // GET: Genero
        public async Task<IActionResult> Index(string filtro)
        {
            var videojuegos = _generoService.GetAll();
            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
            {
                videojuegos = videojuegos.Where(v => v.Nombre.ToLower().Contains(filtro.ToLower())||
                v.Descripcion.ToLower().Contains(filtro.ToLower())
                ).ToList();
            }
            return View(videojuegos.ToList());
        }
        [Authorize(Roles = "Administrador,Operador,Visualizador")]
        // GET: Genero/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _generoService.GetById(id.Value);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Genero/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nombre,Descripcion")] GeneroViewModel genero)
        {
            if (ModelState.IsValid)
            {
                await _generoService.Create(new Genero{
                    Nombre=genero.Nombre,
                    Descripcion=genero.Descripcion,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        [Authorize(Roles = "Administrador,Operador")]
        // GET: Genero/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _generoService.GetById(id.Value);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Genero/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,VideojuegoId")] GeneroViewModel genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _generoService.Update(new Genero{
                    Id=genero.Id,
                    Nombre=genero.Nombre,
                    Descripcion=genero.Descripcion,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(genero);
        }

        [Authorize(Roles = "Administrador,Operador")]
        // GET: Genero/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _generoService.GetById(id.Value);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // POST: Genero/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _generoService.Delete(id); 
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
          return (_context.Genero?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
