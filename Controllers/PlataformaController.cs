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
using Parcial1.Utils;
using Parcial1.ViewModels;

namespace Parcial1.Controllers
{
    public class PlataformaController : Controller
    {
        private readonly VideojuegoContext _context;
        private readonly IPlataformaService _plataformaService;

        public PlataformaController(VideojuegoContext context, IPlataformaService plataformaService)
        {
            _context = context;
            _plataformaService=plataformaService;
        }

        // GET: Plataforma
        public async Task<IActionResult> Index(string filtro)
        {
            var plataformas = _plataformaService.GettAll();
            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
            {
                plataformas = plataformas.Where(v => v.Nombre.ToLower().Contains(filtro.ToLower())||
                 v.Empresa.ToLower().Contains(filtro.ToLower())).ToList();
            }
            return View(plataformas.ToList());
        }
        [Authorize(Roles = "Administrador,Operador,Visualizador")]
        // GET: Plataforma/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataforma = await _plataformaService.GetById(id.Value);

            return View(plataforma);
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Plataforma/Create
        
        public IActionResult Create()
        {

            return View();
        }

        // POST: Plataforma/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Empresa")] PlataformaViewModel plataforma)
        {
            if (ModelState.IsValid)
            {
                await _plataformaService.Create(new Plataforma{
                    Nombre=plataforma.Nombre,
                    Empresa=plataforma.Empresa,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(plataforma);
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Plataforma/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Plataforma == null)
            {
                return NotFound();
            }

            var plataforma = await _plataformaService.GetById(id.Value);
            if (plataforma == null)
            {
                return NotFound();
            }
            return View(plataforma);
        }

        // POST: Plataforma/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Empresa")] PlataformaViewModel plataforma)
        {
            if (id != plataforma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _plataformaService.Update(new Plataforma{
                    Id=plataforma.Id,
                    Nombre=plataforma.Nombre,
                    Empresa=plataforma.Empresa,
                });
                return RedirectToAction(nameof(Index));
            }
            return View(plataforma);
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Plataforma/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plataforma = await _plataformaService.GetById(id.Value);
            if (plataforma == null)
            {
                return NotFound();
            }

            return View(plataforma);
        }

        // POST: Plataforma/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _plataformaService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool PlataformaExists(int id)
        {
          return (_context.Plataforma?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}