using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vergedb.Data;
using vergedb.Models;

namespace vergedb.Controllers
{
    [Route("api/Performances")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly VergedbContext _context;

        public PerformancesController(VergedbContext context)
        {
            _context = context;
        }

        // GET: api/Performances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performance>>> GetPerformance()
        {
          if (_context.Performance == null)
          {
              return NotFound();
          }
            return await _context.Performance.ToListAsync();
        }

        // GET: api/Performances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> GetPerformance(int id)
        {
          if (_context.Performance == null)
          {
              return NotFound();
          }
            var performance = await _context.Performance.FindAsync(id);

            if (performance == null)
            {
                return NotFound();
            }

            return performance;
        }

        // PUT: api/Performances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformance(int id, Performance performance)
        {
            if (id != performance.PerformanceID)
            {
                return BadRequest();
            }

            _context.Entry(performance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(id))
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

        // POST: api/Performances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Performance>> PostPerformance(Performance performance)
        {
          if (_context.Performance == null)
          {
              return Problem("Entity set 'VergedbContext.Performance'  is null.");
          }
            _context.Performance.Add(performance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformance", new { id = performance.PerformanceID }, performance);
        }

        // DELETE: api/Performances/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            if (_context.Performance == null)
            {
                return NotFound();
            }
            var performance = await _context.Performance.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }

            _context.Performance.Remove(performance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerformanceExists(int id)
        {
            return (_context.Performance?.Any(e => e.PerformanceID == id)).GetValueOrDefault();
        }
    }
}
