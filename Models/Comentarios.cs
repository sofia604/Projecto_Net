namespace Projecto.Models
{
    public class Comentarios
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaComentario { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        public int Rating { get; set; } // Rating del 1 al 5
        public string Usuario { get; set; } // Nombre del usuario que comenta   

    }
}
