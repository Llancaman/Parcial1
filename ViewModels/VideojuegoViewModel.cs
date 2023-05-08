using Parcial1.Utils;
using System.ComponentModel.DataAnnotations;
namespace Parcial1.ViewModels;

public class ViedeojuegoViewMOdel
{
    public int Id { get; set; }
    [Required(ErrorMessage ="Tiene que completarse este campo")]
    public string Nombre { get; set; }
    [Required(ErrorMessage ="Tiene que completarse este campo")]
    public string Desarrollador { get; set; }
    [Required(ErrorMessage ="Tiene que completarse este campo")]
    public ViedeojuegoType RestriccionEdad { get; set; }
    [Required(ErrorMessage ="Tiene que completarse este campo")]
    [Range(2000,25000,ErrorMessage ="El rango entre precios es de $2000 hasta $25000")]
    public int Precio { get; set; }
    [Required(ErrorMessage ="Tiene que completarse este campo")]
    public int GeneroId{get;set;}
}