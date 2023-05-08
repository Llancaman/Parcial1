using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Parcial1.Data;
using Parcial1.Models;
using Parcial1.Utils;
using Parcial1.ViewModels;

namespace Parcial1.Controllers
{
    public class VideojuegoController : Controller
    {
        private readonly VideojuegoContext _context;

        public VideojuegoController(VideojuegoContext context)
        {
            _context = context;
        }

        // GET: Videojuego
        public IActionResult Index(string filtro)
        {           
            var videojuegos = _context.Videojuego.Include(v => v.Genero).AsEnumerable();
            if(!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro)){
                videojuegos=videojuegos.Where(v=>v.Nombre.ToLower().Contains(filtro.ToLower()));
            }
            return View(videojuegos.ToList());
        }

        // GET: Videojuego/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Videojuego == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuego
                .Include(v => v.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videojuego == null)
            {
                return NotFound();
            }

            return View(videojuego);
        }

        // GET: Videojuego/Create
        public IActionResult Create()
        {
            // _context.Genero.Add(new Genero{Nombre="Aventura",Descripcion="Explora mundos"});
            // _context.Genero.Add(new Genero{Nombre="Shooter",Descripcion="Juegos de disparos"});
            // _context.SaveChanges();

            ViewBag.ViedeojuegoType = Enum.GetValues(typeof(ViedeojuegoType)).Cast<ViedeojuegoType>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((int)e).ToString()
            }).ToList();

            ViewBag.Generos=new SelectList(_context.Genero.Select(g=>new{
                Id=g.Id,
                Nombre=g.Nombre,

            }),"Id", "Nombre");
            return View();
        }

        // POST: Videojuego/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Desarrollador,RestriccionEdad,Precio,GeneroId")] ViedeojuegoViewMOdel videojuego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Videojuego{
                    GeneroId=videojuego.GeneroId,
                    Nombre=videojuego.Nombre,
                    Desarrollador=videojuego.Desarrollador,
                    RestriccionEdad=videojuego.RestriccionEdad,
                    Precio=videojuego.Precio,
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Id", videojuego.GeneroId);
            return View(videojuego);
        }

        // GET: Videojuego/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Videojuego == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuego.FindAsync(id);
            if (videojuego == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Id", videojuego.GeneroId);
            return View(videojuego);
        }

        // POST: Videojuego/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Desarrollador,RestriccionEdad,Precio,GeneroId")] Videojuego videojuego)
        {
            if (id != videojuego.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videojuego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideojuegoExists(videojuego.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Id", videojuego.GeneroId);
            return View(videojuego);
        }

        // GET: Videojuego/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Videojuego == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuego
                .Include(v => v.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videojuego == null)
            {
                return NotFound();
            }

            return View(videojuego);
        }

        // POST: Videojuego/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Videojuego == null)
            {
                return Problem("Entity set 'VideojuegoContext.Videojuego'  is null.");
            }
            var videojuego = await _context.Videojuego.FindAsync(id);
            if (videojuego != null)
            {
                _context.Videojuego.Remove(videojuego);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideojuegoExists(int id)
        {
          return (_context.Videojuego?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
