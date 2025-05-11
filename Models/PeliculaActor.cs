using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Projecto.Models
{
    public class PeliculaActor
    {
        [Display(Name = "Película")]  
        public int PeliculaId { get; set; }

        [ValidateNever]
        public Pelicula Pelicula { get; set; }
        [Display(Name = "Actor")]
        public int ActorId { get; set; }

        [ValidateNever]
        public Actor Actor { get; set; }

        [Required(ErrorMessage = "El Rol de actor/actriz en la película es requerido.")]
        public string Rol { get; set; } // Ejemplo: "Protagonista", "Secundario", etc.
    }
}
