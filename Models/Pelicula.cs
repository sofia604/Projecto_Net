namespace Projecto.Models
{
  public class Pelicula
  {
    public int Id { get; set; }
    public string Titulo { get; set; }

    public string Sinopsis { get; set; }

    public int Duracion { get; set; } // en minutos

    public DateTime FechaEstreno { get; set; }

    public string ImagenRuta { get; set; }


    // FK a Género
    public int GeneroId { get; set; }
    public Genero Genero { get; set; }

    // FK a Director
    public int DirectorId { get; set; }
    public Director Director { get; set; }

    public ICollection<Actor> Actores { get; set; } = new List<Actor>();
  }

}
