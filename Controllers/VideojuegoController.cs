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
using Parcial1.Services;
using Parcial1.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Parcial1.Controllers
{
    public class VideojuegoController : Controller
    {
        private readonly VideojuegoContext _context;
        private readonly IVideojuegoService _videojuegoService;

        public VideojuegoController(VideojuegoContext context, IVideojuegoService videojuegoService)
        {
            _context = context;
            _videojuegoService = videojuegoService;
        }

        // GET: Videojuego
        public IActionResult Index(string filtro)
        {
            var videojuegos = _videojuegoService.GetAll();
            if (!string.IsNullOrEmpty(filtro) && !string.IsNullOrWhiteSpace(filtro))
            {
                videojuegos = videojuegos.Where(v => v.Nombre.ToLower().Contains(filtro.ToLower()) ||
                v.Desarrollador.ToLower().Contains(filtro.ToLower()) ||
                v.Genero.Nombre.ToLower().Contains(filtro.ToLower()) ||
                v.RestriccionEdad.ToString().ToLower().Contains(filtro.ToLower()) ||
                v.Precio.ToString().ToLower().Contains(filtro.ToLower())
                ).ToList();
            }
            return View(videojuegos.ToList());
        }
        [Authorize(Roles = "Administrador,Operador,Visualizador")]
        // GET: Videojuego/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Videojuego == null)
            {
                return NotFound();
            }

            var videojuego = await _videojuegoService.GetById(id.Value);
            if (videojuego == null)
            {
                return NotFound();
            }
            return View(videojuego);
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Videojuego/Create  
        public IActionResult Create()
        {

            ViewBag.ViedeojuegoType = Enum.GetValues(typeof(ViedeojuegoType)).Cast<ViedeojuegoType>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((int)e).ToString()
            }).ToList();

            ViewBag.Generos = new SelectList(_context.Genero.Select(g => new
            {
                Id = g.Id,
                Nombre = g.Nombre,

            }), "Id", "Nombre");

            ViewBag.Plataformas = new SelectList(_context.Plataforma.Select(g => new
            {
                Id = g.Id,
                Nombre = g.Nombre,

            }), "Id", "Nombre");
            return View();
        }

        // POST: Videojuego/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ViedeojuegoViewMOdel videojuego)
        {
            if (ModelState.IsValid)
            {
                await _videojuegoService.Create(new Videojuego
                {
                    GeneroId = videojuego.GeneroId,
                    Nombre = videojuego.Nombre,
                    Desarrollador = videojuego.Desarrollador,
                    RestriccionEdad = videojuego.RestriccionEdad,
                    Precio = videojuego.Precio,

                    
                },videojuego.Plataformas);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Id", videojuego.GeneroId);
            return View(videojuego);
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Videojuego/Edit/5    
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var videojuego = await _videojuegoService.GetById(id.Value);
            if (videojuego == null)
            {
                return NotFound();
            }
            var videojuegos = new ViedeojuegoViewMOdel()
            {
                GeneroId = videojuego.GeneroId,
                Nombre = videojuego.Nombre,
                Desarrollador = videojuego.Desarrollador,
                RestriccionEdad = videojuego.RestriccionEdad,
                Precio = videojuego.Precio,
            };
            ViewBag.ViedeojuegoType = Enum.GetValues(typeof(ViedeojuegoType)).Cast<ViedeojuegoType>().Select(e => new SelectListItem
            {
                Text = e.ToString(),
                Value = ((int)e).ToString()
            }).ToList();

            ViewBag.Generos = new SelectList(_context.Genero.Select(g => new
            {
                Id = g.Id,
                Nombre = g.Nombre,

            }), "Id", "Nombre");
            ViewBag.Plataformas = new SelectList(_context.Plataforma.Select(g => new
            {
                Id = g.Id,
                Nombre = g.Nombre,

            }), "Id", "Nombre");
            return View(videojuegos);
        }

        // POST: Videojuego/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,ViedeojuegoViewMOdel videojuego)
        {
            if (id != videojuego.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _videojuegoService.Update(new Videojuego
                {
                    Id = videojuego.Id,
                    GeneroId = videojuego.GeneroId,
                    Nombre = videojuego.Nombre,
                    Desarrollador = videojuego.Desarrollador,
                    RestriccionEdad = videojuego.RestriccionEdad,
                    Precio = videojuego.Precio,

                },videojuego.Plataformas);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Id", videojuego.GeneroId);
            return View(videojuego);
        }
        [Authorize(Roles = "Administrador,Operador")]
         public async Task<IActionResult> BlackFriday(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var videojuego = await _videojuegoService.GetById(id.Value);
            if (videojuego == null)
            {
                return NotFound();
            }
            var viewModel = new VideojuegoBlackFridayViewModel{
                Id=videojuego.Id,
                Nombre=videojuego.Nombre,
                Precio=videojuego.Precio,
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlackFriday(VideojuegoBlackFridayViewModel model, List<int> id)
        {
            var videojuego = await _videojuegoService.GetById(model.Id);
            if (videojuego == null)
            {
                return NotFound();
            }
            var cuenta = videojuego.Precio - (model.Porcentaje * videojuego.Precio/100);           
            if(model.Porcentaje>0 && cuenta>0)
            {
            videojuego.Precio= cuenta;
            await _videojuegoService.BlackFriday(videojuego);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrador,Operador")]
        // GET: Videojuego/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _videojuegoService.GetById(id.Value);
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
            await _videojuegoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VideojuegoExists(int id)
        {
            return (_context.Videojuego?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}