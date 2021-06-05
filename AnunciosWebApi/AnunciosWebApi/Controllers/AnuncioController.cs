using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnunciosWebApi.DBContext;
using AnunciosWebApi.Models;

namespace AnunciosWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase 
    {
        private readonly AnuncioDbContext _context;

        public AnuncioController(AnuncioDbContext context)
        {
            _context = context;
        }

        // GET: Anuncio
        [HttpGet("Listar")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Anuncios.ToListAsync());
        }

        // GET: Anuncio/Details/5
        [HttpGet("Detalle")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anuncio = await _context.Anuncios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anuncio == null)
            {
                return NotFound();
            }

            return Ok(anuncio);
        }

        // POST: Anuncio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Registrar")]
        public async Task<IActionResult> Create([Bind("Id,Titulo,IdTipo,Precio,Imagen")] Anuncio anuncio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anuncio);
                await _context.SaveChangesAsync();
                return Ok(anuncio);
            }
            return BadRequest("No se creo el Anuncio");
        }

        // POST: Anuncio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Modificar")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,IdTipo,Precio,Imagen")] Anuncio anuncio)
        {
            if (id != anuncio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anuncio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnuncioExists(anuncio.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(nameof(Index));
            }
            return BadRequest("No se modifico el Anuncio");
        }


        // POST: Anuncio/Delete/5
        [HttpPost("Borrar"), ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anuncio = await _context.Anuncios.FindAsync(id);
            _context.Anuncios.Remove(anuncio);
            await _context.SaveChangesAsync();
            return Ok("Se borro exitosamente");
        }

        private bool AnuncioExists(int id)
        {
            return _context.Anuncios.Any(e => e.Id == id);
        }
    }
}
