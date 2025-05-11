using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Models
{
  public class Genero
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del género es requerido.")]
    public string Nombre { get; set; }

    [DisplayName("Descripción")]
    [Required(ErrorMessage = "El campo descripción es requerido.")]
    public string Descripcion { get; set; }

    }
}
