using Parcial1.Models;

namespace Parcial1.Services;

public interface IVideojuegoService
{
   Task Create(Videojuego obj); 
   List<Videojuego> GetAll();
   Task Update(Videojuego obj);
   Task Delete(int id);
   Task<Videojuego> GetById(int id);
}