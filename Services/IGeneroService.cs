using Parcial1.Models;

namespace Parcial1.Services;

public interface IGeneroService
{
   Task Create(Genero obj);
   List<Genero> GetAll();
   Task Update(Genero obj);
   Task Delete(int id);
   Task<Genero> GetById(int id);
}