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
    public class CorreoController : ControllerBase
    {
        private readonly AnuncioDbContext _context;

        public CorreoController(AnuncioDbContext context)
        {
            _context = context;
        }

        // GET: Correo
        [HttpGet("Listar")]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Correos.ToListAsync());
        }

        // GET: Correo/Details/5
        [HttpGet("Detalle")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var correo = await _context.Correos
                .FirstOrDefaultAsync(m => m.IdCorreo == id);
            if (correo == null)
            {
                return NotFound();
            }

            return Ok(correo);
        }


        // POST: Correo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Registrar")]
       // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCorreo,Nombre,Correo1,Mensaje")] Correo correo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(correo);
                await _context.SaveChangesAsync();
                return Ok(correo);
            }
            return BadRequest("No se creo el Correo");
        }

        // POST: Correo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Modificar")]
        public async Task<IActionResult> Edit(int id, [Bind("IdCorreo,Nombre,Correo1,Mensaje")] Correo correo)
        {
            if (id != correo.IdCorreo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(correo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorreoExists(correo.IdCorreo))
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
            return Ok(correo);
        }

        // POST: Correo/Delete/5
        [HttpPost("Borrar"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var correo = await _context.Correos.FindAsync(id);
            _context.Correos.Remove(correo);
            await _context.SaveChangesAsync();
            return Ok("Se borro exitosamente");
        }

        private bool CorreoExists(int id)
        {
            return _context.Correos.Any(e => e.IdCorreo == id);
        }
    }
}
