using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VergeDBAPI.Models;
using VergeDBAPI.Validation;

namespace VergeDBAPI.Controllers
{
    [Route("v1/performances")]
    [ApiController]
    public class PerformancesController : ControllerBase
    {
        private readonly VergeDBAPIContext _context;
        private UserData userData;

        public PerformancesController(VergeDBAPIContext context)
        {
            _context = context;
            userData = new UserData();
        }

        // PUT: v1/performances/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformance(int id, Performance performance)
        {
            if (id != performance.PerformanceID)
            {
                return BadRequest();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
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

        // POST: v1/performances
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)}")]
        [HttpPost]
        public async Task<ActionResult<Performance>> PostPerformance([FromForm] PerformanceForm performance)
        {
            if (_context.Performances == null)
            {
                return Problem("Entity set 'VergeDBAPIContext.Performances'  is null.");
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            var drone = await _context.Drones.Where(d => d.DroneUID == performance.DroneUID).FirstOrDefaultAsync();
            performance.DroneID = drone.DroneID;

            _context.Performances.Add(performance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformance", new { id = performance.PerformanceID }, performance);
        }

        // DELETE: v1/performances/5
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformance(int id)
        {
            if (_context.Performances == null)
            {
                return NotFound();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            var performance = await _context.Performances.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }

            _context.Performances.Remove(performance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PerformanceExists(int id)
        {
            return (_context.Performances?.Any(e => e.PerformanceID == id)).GetValueOrDefault();
        }
    }
}
