using Parcial1.Utils;
namespace Parcial1.Models;

public class Videojuego{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Desarrollador { get; set; }
    public ViedeojuegoType RestriccionEdad{get;set;}
    public int Precio { get; set; }
    public int GeneroId{get;set;}
    public Genero Genero{get;set;}

    public virtual List<Plataforma> Plataformas{get;set;}
    
}