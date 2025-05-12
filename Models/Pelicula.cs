using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projecto.Models
{
  public class Pelicula
  {
    public int Id { get; set; }

    [Display(Name = "Título")]
    [Required(ErrorMessage = "El título o nombre de la película es requerido.")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "La sinopsis de la película es requerida.")]
    [Display(Name = "Sinopsis")]
    public string Sinopsis { get; set; }

    [Display(Name = "Duración (minutos)")]
    [Required(ErrorMessage = "La duración de la película (en minutos) es requerida.")]
    [Range(1, 600, ErrorMessage = "La duración debe estar entre 1 y 600 minutos.")]
    public int Duracion { get; set; }


        [Display(Name = "Fecha de Estreno")]
    [Required(ErrorMessage = "La fecha de estreno de la película es requerida.")]
    public DateTime FechaEstreno { get; set; }

    [Display(Name = "Portada")]
    public string? ImagenRuta { get; set; }


    // FK a Género
    [Display(Name = "Género")]
    public int GeneroId { get; set; }

    [ForeignKey("GeneroId")]
    [ValidateNever]
    public Genero Genero { get; set; }

    // FK a Director
    [Display(Name = "Director")]
    [ForeignKey("DirectorId")]
    public int DirectorId { get; set; }
    [ValidateNever]
    public Director Director { get; set; }
    [ValidateNever]
    public ICollection<PeliculaActor> Actores { get; set; } = new List<PeliculaActor>();
    [ValidateNever]
    public ICollection<Comentarios> Comments { get; set; } = new List<Comentarios>();

  
    [NotMapped]
    public IFormFile? ImagenPortada { get; set; }
  }

}
