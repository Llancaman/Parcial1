namespace Parcial1.Models;

public class Genero{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int VideojuegoId{get;set;}

    public virtual List<Videojuego> Videojuegos{get;set;}
}