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
    public void Create(Genero obj)
    {
        throw new NotImplementedException();
    }

    public void Delete(Genero obj)
    {
        throw new NotImplementedException();
    }

    public Genero? GetById(int id)
    {
        var genero =_context.Genero
                .FirstOrDefault(m => m.Id == id);

        return genero;
    }

    public List<Videojuego> GettAll()
    {
        return _context.Genero != null ? 
                          View(_context.Genero.ToListAsync()) :
                          Problem("Entity set 'VideojuegoContext.Genero'  is null.");
    }

    private List<Videojuego> View(object value)
    {
        throw new NotImplementedException();
    }

    private List<Videojuego> Problem(string v)
    {
        throw new NotImplementedException();
    }

    public void Update(Genero obj)
    {
        throw new NotImplementedException();
    }
}