using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projecto.Data;
using Projecto.Models;

namespace Projecto.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly AppDbContext _context;

        public PeliculasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Peliculas
        public async Task<IActionResult> Index(string searchString, int page = 1)
        {
           
            int pageSize = 4;


            var peliculas = _context.Peliculas
                         .Include(p => p.Director)
                         .Include(p => p.Genero)
                         .AsQueryable(); 


            if (!String.IsNullOrEmpty(searchString))
            {
                peliculas = peliculas.Where(p =>
                                       p.Titulo.Contains(searchString) ||
                                       p.Genero.Nombre.Contains(searchString));
             }

            peliculas = peliculas.OrderBy(l => l.Titulo); 

            int totalPeliculas = await peliculas.CountAsync();

            var peliculas2 = await peliculas
                .Skip((page - 1) * pageSize) 
                .Take(pageSize) 
                .ToListAsync();

            ViewData["CurrentPage"] = page;
            ViewData["TotalPages"] = (int)Math.Ceiling(totalPeliculas / (double)pageSize);
            ViewData["CurrentFilter"] = searchString;

            return View(peliculas2);
    }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Director)
                .Include(p => p.Genero)
                .Include(p => p.Actores)
                .ThenInclude(pa => pa.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // GET: Peliculas/Create
        public IActionResult Create()
        {
            ViewBag.DirectorId = new SelectList(_context.Directores.OrderBy(d => d.Nombre).Select(director=> new
                                                      {
                                                        director.Id,
                                                        NombreCompleto = director.Nombre + " " + director.Apellido
                                                      }),
                                                      "Id", "NombreCompleto");
            ViewBag.GeneroId = new SelectList(_context.Generos.OrderBy(g=> g.Nombre), "Id", "Nombre");
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pelicula pelicula)
        {
            if (ModelState.IsValid)
            {
                  if(pelicula.ImagenPortada != null && pelicula.ImagenPortada.Length > 0)
                  {
                      var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(pelicula.ImagenPortada.FileName);
                      var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                      using (var stream= new FileStream(ruta, FileMode.Create))
                      {
                          await pelicula.ImagenPortada.CopyToAsync(stream);
                      }

                      pelicula.ImagenRuta = "/imagenes/" + nombreArchivo;
                  }
                
                  _context.Add(pelicula);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
            }
             ViewBag.DirectorId = new SelectList(_context.Directores.OrderBy(d => d.Nombre).Select(director => new
                                                      {
                                                        director.Id,
                                                        NombreCompleto = director.Nombre + " " + director.Apellido
                                                      }),
                                                      "Id", "NombreCompleto", pelicula.DirectorId);
            ViewBag.GeneroId = new SelectList(_context.Generos, "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directores.OrderBy(d => d.Nombre).Select(pelicula => new
                                                      {
                                                        pelicula.Id,
                                                        NombreCompleto = pelicula.Nombre + " " + pelicula.Apellido
                                                      }),
                                                      "Id", "NombreCompleto", pelicula.DirectorId);



            ViewData["GeneroId"] = new SelectList(_context.Generos.OrderBy(g => g.Nombre), "Id", "Nombre", pelicula.GeneroId);
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pelicula pelicula)
        {
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            var peliculaExistente = await _context.Peliculas.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
            if (peliculaExistente == null) return NotFound();


            if (ModelState.IsValid)
            {
                try
                {
                    if (pelicula.ImagenPortada != null && pelicula.ImagenPortada.Length > 0)
                    {
                        if (!string.IsNullOrEmpty(peliculaExistente.ImagenRuta))
                        {
                          var rutaAnterior = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", peliculaExistente.ImagenRuta.TrimStart('/'));
                              if (System.IO.File.Exists(rutaAnterior))
                              {
                                System.IO.File.Delete(rutaAnterior);
                              }
                        }
                        var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(pelicula.ImagenPortada.FileName);
                        var ruta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagenes", nombreArchivo);

                        using (var stream = new FileStream(ruta, FileMode.Create))
                        {
                          await pelicula.ImagenPortada.CopyToAsync(stream);
                        }

                        //Guardar nueva imagen de ruta
                        pelicula.ImagenRuta = "/imagenes/" + nombreArchivo;
                      }
                    else
                    {
                         pelicula.ImagenRuta = peliculaExistente.ImagenRuta;
                    }
                     _context.Update(pelicula);
                     await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorId"] = new SelectList(_context.Directores.OrderBy(d => d.Nombre).Select(pelicula => new
                                                      {
                                                        pelicula.Id,
                                                        NombreCompleto = pelicula.Nombre + " " + pelicula.Apellido
                                                      }),
                                                      "Id", "NombreCompleto", pelicula.DirectorId);
            ViewData["GeneroId"] = new SelectList(_context.Generos.OrderBy(g => g.Nombre), "Id", "Id", pelicula.GeneroId);
            return View(pelicula);
        }

        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .Include(p => p.Director)
                .Include(p => p.Genero)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula != null)
            {
                if(!string.IsNullOrEmpty(pelicula.ImagenRuta))
                {

                    //Obtener la ruta fisica en el servidor
                    var rutaCompleta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", pelicula.ImagenRuta.TrimStart('/'));

                    if(System.IO.File.Exists(rutaCompleta))
                    {
                        System.IO.File.Delete(rutaCompleta);
                    }
                }
                _context.Peliculas.Remove(pelicula);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }

        public async Task<IActionResult> ObtenerComentarios(int peliculaId)
        {
          var comentarios = await _context.Comentarios
                                    .Where(c => c.PeliculaId == peliculaId)
                                    .OrderByDescending(c => c.FechaComentario)
                                    .ToListAsync();

          return PartialView("_ListaComentariosPartial", comentarios);
        }
  }
  


  }
