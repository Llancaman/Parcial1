using Parcial1.Models;

namespace Parcial1.Services;

public interface IGeneroService
{
   void Create(Genero obj); 
   List<Videojuego> GettAll();
   void Update(Genero obj);
   void Delete(Genero obj);
   Genero? GetById(int id);
}