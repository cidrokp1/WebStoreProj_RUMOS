using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Models;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinhasDeFaturasController : ControllerBase
    {
        private readonly DAL.Data.BD _context;

        public LinhasDeFaturasController(DAL.Data.BD context)
        {
            _context = context;
        }

        // GET: api/LinhasDeFaturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LinhasDeFatura>>> GetLinhasDeFaturas()
        {
            return await _context.LinhasDeFaturas.ToListAsync();
        }

        // GET: api/LinhasDeFaturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LinhasDeFatura>> GetLinhasDeFatura(int id)
        {
            var linhasDeFatura = await _context.LinhasDeFaturas.FindAsync(id);

            if (linhasDeFatura == null)
            {
                return NotFound();
            }

            return linhasDeFatura;
        }

        // PUT: api/LinhasDeFaturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLinhasDeFatura(int id, LinhasDeFatura linhasDeFatura)
        {
            if (id != linhasDeFatura.LinhasDeFaturaId)
            {
                return BadRequest();
            }

            _context.Entry(linhasDeFatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinhasDeFaturaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LinhasDeFaturas
        [HttpPost]
        public async Task<ActionResult<LinhasDeFatura>> PostLinhasDeFatura(LinhasDeFatura linhasDeFatura)
        {
            _context.LinhasDeFaturas.Add(linhasDeFatura);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLinhasDeFatura", new { id = linhasDeFatura.LinhasDeFaturaId }, linhasDeFatura);
        }

        // DELETE: api/LinhasDeFaturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LinhasDeFatura>> DeleteLinhasDeFatura(int id)
        {
            var linhasDeFatura = await _context.LinhasDeFaturas.FindAsync(id);
            if (linhasDeFatura == null)
            {
                return NotFound();
            }

            _context.LinhasDeFaturas.Remove(linhasDeFatura);
            await _context.SaveChangesAsync();

            return linhasDeFatura;
        }


        private bool LinhasDeFaturaExists(int id)
        {
            return _context.LinhasDeFaturas.Any(e => e.LinhasDeFaturaId == id);
        }
    }
}
