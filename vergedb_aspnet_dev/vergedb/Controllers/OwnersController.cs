using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using vergedb.Data;
using vergedb.Models;

namespace vergedb.Controllers
{
    [Route("api/Owners")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly VergedbContext _context;
        private readonly CustomContractResolver jsonResolver;
        private readonly JsonSerializerSettings serializerSettings;

        public OwnersController(VergedbContext context)
        {
            _context = context;
            jsonResolver = new CustomContractResolver();
            serializerSettings = new JsonSerializerSettings();
        }

        // GET: api/Owners
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwner()
        {
          if (_context.Owner == null)
          {
              return NotFound();
          }
            List<Owner> ownerList = await _context.Owner.Select(x => new Owner
            {
                OwnerID = x.OwnerID,
                CompanyName = x.CompanyName,
                NumDrones = x.Drones.Count(),
                Drones = x.Drones
            }).ToListAsync();

            jsonResolver.IgnoreProperty(typeof(Owner), "key", "drones");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(ownerList, serializerSettings)));
        }

        // GET: api/Owners/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            if (_context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.Where(i => i.OwnerID == id).Include(d => d.Drones).SingleAsync();
            owner.NumDrones = owner.Drones.Count();

            if (owner == null)
            {
                return NotFound();
            }

            jsonResolver.IgnoreProperty(typeof(Owner), "key", "drones");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(owner, serializerSettings)));
        }

        // GET: api/Owners/Drones
        [HttpGet("Drones")]
        public async Task<IActionResult> GetDrones()
        {
            if (_context.Owner == null)
            {
                return NotFound();
            }
            var owner = await _context.Owner.Include(d => d.Drones).ToListAsync();

            foreach(Owner o in owner)
            {
                o.NumDrones = o.Drones.Count();
            }

            if (owner == null)
            {
                return NotFound();
            }

            jsonResolver.IgnoreProperty(typeof(Owner), "key");
            jsonResolver.IgnoreProperty(typeof(Drone), "key", "owner_name", "performances", "performance_count");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(owner, serializerSettings)));
        }

        // GET: api/Owners/Drones/Verge
        [HttpGet("Drones/{name}")]
        public async Task<IActionResult> GetDrones(string name)
        {
            if (_context.Owner == null)
            {
                return NotFound();
            }

            var owner = await _context.Owner.Where(i => i.CompanyName.Equals(name)).Include(d => d.Drones).SingleAsync();

            owner.NumDrones = owner.Drones.Count();

            if (owner == null)
            {
                return NotFound();
            }

            jsonResolver.IgnoreProperty(typeof(Owner), "key");
            jsonResolver.IgnoreProperty(typeof(Drone), "key", "owner_name", "performances", "performance_count");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(owner, serializerSettings)));
        }

        // PUT: api/Owners/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(int id, Owner owner)
        {
            if (id != owner.OwnerID)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
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

        // POST: api/Owners
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
          if (_context.Owner == null)
          {
              return Problem("Entity set 'VergedbContext.Owner'  is null.");
          }
            _context.Owner.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.OwnerID }, owner);
        }

        // DELETE: api/Owners/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            if (_context.Owner == null)
            {
                return NotFound();
            }
            var owner = await _context.Owner.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owner.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnerExists(int id)
        {
            return (_context.Owner?.Any(e => e.OwnerID == id)).GetValueOrDefault();
        }
    }
}
