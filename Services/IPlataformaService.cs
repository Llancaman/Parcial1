using Parcial1.Models;

namespace Parcial1.Services;

public interface IPlataformaService
{
   Task Create(Plataforma obj); 
   List<Plataforma> GettAll();
   Task Update(Plataforma obj);
   Task Delete(int id);
   Task<Plataforma> GetById(int id);
}