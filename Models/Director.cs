namespace Projecto.Models
{
    public class Director
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Nacionalidad { get; set; }
        public ICollection<Pelicula> Peliculas { get; set; } = new List<Pelicula>();
    }
}
