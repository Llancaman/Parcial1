namespace Parcial1.Models;

public class Plataforma{
    public int Id{get;set;}
    public string Nombre{get;set;}
    public string Empresa{get;set;}

    public virtual List<Videojuego>Videojuegos{get;set;}
}