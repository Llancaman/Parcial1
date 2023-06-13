using Parcial1.Data;
using Parcial1.Models;
using Microsoft.EntityFrameworkCore;

namespace Parcial1.Services;

public class GeneroService : IGeneroService
{
    private readonly VideojuegoContext _context;

    public GeneroService(VideojuegoContext context)
    {
        _context = context;
    }
    public async Task Create(Genero obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var genero = await _context.Genero.FindAsync(id);
        if (genero != null)
        {
            _context.Genero.Remove(genero);
        }

        await _context.SaveChangesAsync();
    }

    public async Task<Genero> GetById(int id)
    {
        var genero = await _context.Genero
                .FirstOrDefaultAsync(m => m.Id == id);

        return genero;
    }

    public List<Genero> GetAll()
    {
        return _context.Genero.Include(v => v.Videojuegos).ToList();
    }

    public async Task Update(Genero obj)
    {
            _context.Update(obj);
            await _context.SaveChangesAsync();
    }
}