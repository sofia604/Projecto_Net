using System.ComponentModel.DataAnnotations;

namespace Projecto.Models
{
    public class PeliculaActor
    {
        [Display(Name = "Película")]  
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        [Display(Name = "Actor")]
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public string Rol { get; set; } // Ejemplo: "Protagonista", "Secundario", etc.
    }
}
