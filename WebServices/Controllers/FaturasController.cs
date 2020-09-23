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
    public class FaturasController : ControllerBase
    {
        private readonly DAL.Data.BD _context;

        public FaturasController(DAL.Data.BD context)
        {
            _context = context;
        }

        // GET: api/Faturas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fatura>>> GetFaturas()
        {
            return await _context.Faturas
                .Include(f => f.LinhasDeFatura)
                .Include(f => f.Empregado)
                .ToListAsync();
        }

        // GET: api/Faturas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fatura>> GetFatura(int id)
        {
            var fatura = await _context.Faturas
                .Include(f => f.Empregado)
                .Include(f => f.LinhasDeFatura)
                .ThenInclude(f => f.Produto)
                .FirstOrDefaultAsync(e => e.FaturaId == id);

            if (fatura == null)
            {
                return NotFound();
            }

            return fatura;
        }

        // PUT: api/Faturas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFatura(int id, Fatura fatura)
        {
            if (id != fatura.FaturaId)
            {
                return BadRequest();
            }

            _context.Entry(fatura).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaturaExists(id))
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

        // POST: api/Faturas
        [HttpPost]
        public async Task<IActionResult> PostFatura(List<Fatura> listFaturas)
        {
            try
            {
                foreach (var fatura in listFaturas)
                {
                    int i = 0;
                    foreach (var linha in fatura.LinhasDeFatura)
                    {
                        //Se nao existir a linha de fatura, cria uma na BD e associa essa nova linha á fatura a ser criada
                        if (!_context.LinhasDeFaturas.Any(e => e.LinhasDeFaturaId == linha.LinhasDeFaturaId))
                        {
                            _context.LinhasDeFaturas.Add(linha);
                            await _context.SaveChangesAsync();
                            fatura.LinhasDeFatura.ElementAt(i).LinhasDeFaturaId = linha.LinhasDeFaturaId;
                        }
                        i++;
                    }

                    //Se nao existir um empregado com o respetivo email, cria um na BD e associa esse novo empregado á fatura a ser criada
                    var empregado = _context.Empregados.FirstOrDefault(e => e.Email == fatura.Empregado.Email);
                    if (empregado == null)
                    {
                        empregado = fatura.Empregado;
                        _context.Empregados.Add(empregado);
                        await _context.SaveChangesAsync();

                    }
                    fatura.Empregado = empregado;
                    fatura.EmpregadoId = empregado.EmpregadoId;

                    _context.Faturas.Add(fatura);
                    await _context.SaveChangesAsync();
                }


                return Ok("" + listFaturas.Count() + " Faturas foram inseridas com sucesso.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
        }

        // DELETE: api/Faturas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fatura>> DeleteFatura(int id)
        {
            var fatura = await _context.Faturas
               .Include(f => f.Empregado)
               .Include(f => f.LinhasDeFatura)
               .ThenInclude(f => f.Produto)
               .FirstOrDefaultAsync(e => e.FaturaId == id);
            if (fatura == null)
            {
                return NotFound();
            }

            _context.Faturas.Remove(fatura);
            await _context.SaveChangesAsync();

            return fatura;
        }

        private bool FaturaExists(int id)
        {
            return _context.Faturas.Any(e => e.FaturaId == id);
        }
    }
}
