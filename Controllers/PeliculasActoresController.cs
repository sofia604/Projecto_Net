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
  public class PeliculasActoresController : Controller
  {
    private readonly AppDbContext _context;

    public PeliculasActoresController(AppDbContext context)
    {
      _context = context;
    }

    // GET: PeliculasActores
 
    public async Task<IActionResult> Index(string searchString, int page = 1)
    {
      
      int pageSize = 5;


      var peliculasActores = _context.PeliculasActores.Include(p => p.Actor)
                              .Include(p => p.Pelicula)
                              .AsQueryable(); 


      if (!String.IsNullOrEmpty(searchString))
      {
        peliculasActores = peliculasActores.Where(p =>
                               p.Pelicula.Titulo.Contains(searchString) ||
                               p.Actor.Nombre.Contains(searchString)||
                               p.Actor.Apellido.Contains(searchString)
                               );
      }

      peliculasActores = peliculasActores.OrderBy(l => l.Pelicula.Titulo);

      int totalPeliculas = await peliculasActores.CountAsync();

      var peliculasActores2 = await peliculasActores
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .ToListAsync();

      ViewData["CurrentPage"] = page;
      ViewData["TotalPages"] = (int)Math.Ceiling(totalPeliculas / (double)pageSize);
      ViewData["CurrentFilter"] = searchString;

      return View(peliculasActores2);
    }


    // GET: PeliculasActores/Details/5
    [HttpGet("PeliculasActores/Details/{peliculaId}/{actorId}")]
    public async Task<IActionResult> Details(int? peliculaId, int? actorId)
    {
      if (peliculaId == null || actorId == null)
      {
        return NotFound();
      }


      var peliculaActor = await _context.PeliculasActores
          .Include(p => p.Actor)
          .Include(p => p.Pelicula)
          .FirstOrDefaultAsync(m => m.PeliculaId == peliculaId && m.ActorId == actorId);
      if (peliculaActor == null)
      {
        return NotFound();
      }

      return View(peliculaActor);
    }

    // GET: PeliculasActores/Create
    public IActionResult Create()
    {
      ViewData["ActorId"] = new SelectList(_context.Actores.OrderBy(l => l.Nombre)
                                  .Select(actor => new
                                  {
                                    actor.Id,
                                    NombreCompleto = actor.Nombre + " " + actor.Apellido
                                  }),
                                "Id", "NombreCompleto"
                                );


      ViewData["PeliculaId"] = new SelectList(_context.Peliculas.OrderBy(l => l.Titulo), "Id", "Titulo");
      return View();
    }

    // POST: PeliculasActores/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PeliculaId,ActorId,Rol")] PeliculaActor peliculaActor)
    {
      if (ModelState.IsValid)
      {
        _context.Add(peliculaActor);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      ViewData["ActorId"] = new SelectList(_context.Actores.OrderBy(l => l.Nombre)
                            .Select(actor => new
                            {
                              actor.Id,
                              NombreCompleto = actor.Nombre + " " + actor.Apellido
                            }),
                          "Id", "NombreCompleto"
                          );


      ViewData["PeliculaId"] = new SelectList(_context.Peliculas.OrderBy(l => l.Titulo), "Id", "Titulo");
      return View(peliculaActor);
    }


    [HttpGet("PeliculasActores/Edit/{peliculaId}/{actorId}")]
    public async Task<IActionResult> Edit(int? peliculaId, int? actorId)
    {
      if (peliculaId == null || actorId == null)
      {
        return NotFound();
      }

      var peliculaActor = await _context.PeliculasActores
          .FirstOrDefaultAsync(pa => pa.PeliculaId == peliculaId && pa.ActorId == actorId);

      if (peliculaActor == null)
      {
        return NotFound();
      }

      ViewData["ActorId"] = new SelectList(
                                _context.Actores.Select(a => new
                                {
                                  a.Id,
                                  NombreCompleto = a.Nombre + " " + a.Apellido
                                }),
                                "Id", "NombreCompleto",
                                peliculaActor.ActorId
                                );
      ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", peliculaActor.PeliculaId);
      return View(peliculaActor);
    }

    // POST: PeliculasActores/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditRol([Bind("PeliculaId,ActorId,Rol")] PeliculaActor peliculaActor)
    {
      
      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(peliculaActor);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!PeliculaActorExists(peliculaActor.PeliculaId))
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
      ViewData["ActorId"] = new SelectList(
                               _context.Actores.Select(a => new
                               {
                                 a.Id,
                                 NombreCompleto = a.Nombre + " " + a.Apellido
                               }),
                               "Id", "NombreCompleto",
                               peliculaActor.ActorId
                               );
      ViewData["PeliculaId"] = new SelectList(_context.Peliculas, "Id", "Titulo", peliculaActor.PeliculaId);
      return View(peliculaActor);
    }

    // GET: PeliculasActores/Delete/5
    [HttpGet("PeliculasActores/Delete/{peliculaId}/{actorId}")]
    public async Task<IActionResult> Delete(int? peliculaId, int? actorId)
    {
      if (peliculaId == null || actorId == null)
      {
        return NotFound();
      }

      var peliculaActor = await _context.PeliculasActores
                .Include(p => p.Actor)
                .Include(p => p.Pelicula)
                .FirstOrDefaultAsync(m => m.PeliculaId == peliculaId && m.ActorId == actorId);
      if (peliculaActor == null)
      {
        return NotFound();
      }

      return View(peliculaActor);
    }

    // POST: PeliculasActores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? PeliculaId, int? ActorId)
    {
      if (PeliculaId == null || ActorId == null)
      {
        return NotFound();
      }

      var peliculaActor = await _context.PeliculasActores
                                .AsNoTracking()
                                .FirstOrDefaultAsync(pa => pa.PeliculaId == PeliculaId && pa.ActorId == ActorId);
    

      if (peliculaActor != null)
      {
        _context.PeliculasActores.Remove(peliculaActor);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool PeliculaActorExists(int id)
    {
      return _context.PeliculasActores.Any(e => e.PeliculaId == id);
    }
  }
}
