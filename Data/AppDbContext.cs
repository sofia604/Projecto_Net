using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pelicula> Peliculas { get; set; }

    public DbSet<Genero> Generos { get; set; }
  }

}
