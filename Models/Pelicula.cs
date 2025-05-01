using System.ComponentModel.DataAnnotations;

namespace Projecto.Models
{
  public class Pelicula
  {
    public int Id { get; set; }

    [Display(Name = "Título")]
    public string Titulo { get; set; }

    [Display(Name = "Sinopsis")]
    public string Sinopsis { get; set; }

    [Display(Name = "Duración (minutos)")]
    public int Duracion { get; set; } // en minutos

    [Display(Name = "Fecha de Estreno")]
    public DateTime FechaEstreno { get; set; }

    [Display(Name = "Portada")]
    public string ImagenRuta { get; set; }


    // FK a Género
    [Display(Name = "Género")]
    public int GeneroId { get; set; }
    public Genero Genero { get; set; }

    // FK a Director
    [Display(Name = "Director")]
    public int DirectorId { get; set; }
    public Director Director { get; set; }

    public ICollection<PeliculaActor> Actores { get; set; } = new List<PeliculaActor>();
    public ICollection<Comentarios> Comments { get; set; } = new List<Comentarios>();
  }

}
