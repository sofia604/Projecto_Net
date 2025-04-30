namespace Projecto.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public ICollection<PeliculaActor> Peliculas { get; set; } = new List<PeliculaActor>();
    }
}
