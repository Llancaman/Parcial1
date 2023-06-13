using Parcial1.Data;
using Parcial1.Models;
using Microsoft.EntityFrameworkCore;

namespace Parcial1.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly VideojuegoContext _context;

    public VideojuegoService(VideojuegoContext context)
    {
        _context = context;
    }
    public async Task Create(Videojuego obj, List<int> ids)
    {
        _context.Add(obj);
        obj.Plataformas=new List<Plataforma>();
        foreach (var item in ids)
        {
            var plataforma= _context.Plataforma.Find(item);
            obj.Plataformas.Add(plataforma);
        }

        await _context.SaveChangesAsync();

    }

    public async Task Delete(int id)
    {
        var videojuego = await _context.Videojuego.FindAsync(id);
        if (videojuego != null)
        {
            _context.Videojuego.Remove(videojuego);
        }

        await _context.SaveChangesAsync();

    }

    public async Task<Videojuego> GetById(int id)
    {
        var videojuego = await _context.Videojuego
        .Include(v => v.Genero)
        .Include(v => v.Plataformas)
        .FirstOrDefaultAsync(m => m.Id == id);
        return videojuego;
    }

    public List<Videojuego> GetAll()
     {
        return _context.Videojuego.Include(v => v.Genero).ToList();
     }

    public async Task Update(Videojuego obj, List<int> ids)
    {
        _context.Update(obj);
        await _context.SaveChangesAsync();

    }

    public async Task BlackFriday(Videojuego obj)
    {
        _context.Update(obj);
        await _context.SaveChangesAsync();

    }
}