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
    public class TipoController : ControllerBase
    {
        private readonly AnuncioDbContext _context;

        public TipoController(AnuncioDbContext context)
        {
            _context = context;
        }

        // GET: Tipo
        [HttpGet("Listar")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Tipos.ToListAsync());
        }

      
        // GET: Tipo/Details/5
        [HttpGet("Detalle")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipo = await _context.Tipos
                .FirstOrDefaultAsync(m => m.Id_Tipo == id);
            if (tipo == null)
            {
                return NotFound();
            }

            return Ok(tipo);
        }


        // POST: Tipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Registrar")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTipo,Tipo1")] Tipo tipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipo);
                await _context.SaveChangesAsync();
                return Ok(tipo);
            }
            return BadRequest("No se creo el Tipo");
        }

        // POST: Tipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Modificar")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTipo,Tipo1")] Tipo tipo)
        {
            if (id != tipo.Id_Tipo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoExists(tipo.Id_Tipo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                 return Ok(tipo);
            }
            return BadRequest("No se modifico el Tipo");
        }

        // POST: Tipo/Delete/5
        [HttpPost("Borrar"), ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipo = await _context.Tipos.FindAsync(id);
            _context.Tipos.Remove(tipo);
            await _context.SaveChangesAsync();
            return Ok("Se borro exitosamente");
        }

        private bool TipoExists(int id)
        {
            return _context.Tipos.Any(e => e.Id_Tipo == id);
        }
    }
}
