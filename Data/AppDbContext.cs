using Microsoft.EntityFrameworkCore;
using Projecto.Models;

namespace Projecto.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pelicula> Peliculas { get; set; }

    public DbSet<Genero> Generos { get; set; }

    public DbSet<Actor> Actores { get; set; }

    public DbSet<Director> Directores { get; set; }

    public DbSet<Comentarios> Comentarios { get; set; }
    public DbSet<PeliculaActor> PeliculasActores { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      // Clave primaria compuesta
      modelBuilder.Entity<PeliculaActor>()
          .HasKey(pa => new { pa.PeliculaId, pa.ActorId });

      // Relación con Pelicula
      modelBuilder.Entity<PeliculaActor>()
          .HasOne(pa => pa.Pelicula)
          .WithMany(p => p.Actores)
          .HasForeignKey(pa => pa.PeliculaId);

      // Relación con Actor
      modelBuilder.Entity<PeliculaActor>()
          .HasOne(pa => pa.Actor)
          .WithMany(a => a.Peliculas)
          .HasForeignKey(pa => pa.ActorId);

    }
  }

}
