using Parcial1.Data;
using Parcial1.Models;
using Microsoft.EntityFrameworkCore;

namespace Parcial1.Services;

public class PlataformaService : IPlataformaService
{
    private readonly VideojuegoContext _context;

    public PlataformaService(VideojuegoContext context)
    {
        _context = context;
    }
    public async Task Create(Plataforma obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var plataforma = await _context.Plataforma.FindAsync(id);
        if (plataforma != null)
        {
            _context.Plataforma.Remove(plataforma);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Plataforma> GetById(int id)
    {
        var plataforma = _context.Plataforma
                .Include(m => m.Videojuegos)
                .FirstOrDefault(m => m.Id == id);

        return plataforma;
    }

    public List<Plataforma> GettAll()
    {
        return _context.Plataforma.Include(v => v.Videojuegos).ToList();
    }

    public async Task Update(Plataforma obj)
    {
            _context.Update(obj);
            await _context.SaveChangesAsync();
    }
}