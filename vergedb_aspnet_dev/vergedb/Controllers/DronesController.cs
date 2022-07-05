﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using vergedb.Data;
using vergedb.Models;

namespace vergedb.Controllers
{
    [Route("api/Drones")]
    [ApiController]
    public class DronesController : ControllerBase
    {
        private readonly VergedbContext _context;
        private readonly CustomContractResolver jsonResolver;
        private readonly JsonSerializerSettings serializerSettings;

        public DronesController(VergedbContext context)
        {
            _context = context;
            jsonResolver = new CustomContractResolver();
            serializerSettings = new JsonSerializerSettings();
        }

        // GET: api/Drones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drone>>> GetDrone()
        {
            if (_context.Drone == null)
            {
                return NotFound();
            }

            List<Drone> droneList = await _context.Drone.Select(x => new Drone
                                                        {
                                                            DroneID = x.DroneID,
                                                            DroneUID = x.DroneUID,
                                                            FaaId = x.FaaId,
                                                            PixHardware = x.PixHardware,
                                                            PerformanceCount = x.Performances.Count(),
                                                            Performances = x.Performances,
                                                            OwnerName = x.Owner.CompanyName
                                                        }).ToListAsync();

            jsonResolver.IgnoreProperty(typeof(Drone), "key", "performances");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(droneList, serializerSettings)));
        }

        // GET: api/Drones/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDrone(int id)
        {
            if (_context.Drone == null)
            {
                return NotFound();
            }

            var drone = await _context.Drone.Where(i => i.DroneID == id).Include(p => p.Performances).Include(o => o.Owner).SingleAsync();
            drone.OwnerName = drone.Owner.CompanyName;

            if(drone.Performances != null)
                drone.PerformanceCount = drone.Performances.Count();
            else
                drone.PerformanceCount = 999999;

            if (drone == null)
            {
                return NotFound();
            }

            jsonResolver.IgnoreProperty(typeof(Drone), "key", "performances");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(drone, serializerSettings)));
        }

        // GET: api/Drones/Performances
        [HttpGet("Performances")]
        public async Task<IActionResult> GetPerformances()
        {
            if (_context.Drone == null)
            {
                return NotFound();
            }
            var drone = await _context.Drone.Include(p => p.Performances).ToListAsync();

            if (drone == null)
            {
                return NotFound();
            }

            jsonResolver.IgnoreProperty(typeof(Drone), "key", "faa_id", "cube_version", "performance_count", "owner_name");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(drone, serializerSettings)));
        }

        // GET: api/Drones/Performances/5
        [HttpGet("Performances/{id}")]
        public async Task<IActionResult> GetPerformances(int id)
        {
            if (_context.Drone == null)
            {
                return NotFound();
            }
            var drone = await _context.Drone.Where(i => i.DroneID == id).Include(p => p.Performances).ToListAsync();

            if (drone == null)
            {
                return NotFound();
            }

            jsonResolver.IgnoreProperty(typeof(Drone), "key", "faa_id", "cube_version", "performance_count", "owner_name");
            serializerSettings.ContractResolver = jsonResolver;

            return Ok(JsonConvert.DeserializeObject(JsonConvert.SerializeObject(drone, serializerSettings)));
        }

        // PUT: api/Drones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDrone(int id, Drone drone)
        {
            if (id != drone.DroneID)
            {
                return BadRequest();
            }

            _context.Entry(drone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DroneExists(id))
                {
                    return NotFound(); //404
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Drones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Drone>> PostDrone(Drone drone)
        {
          if (_context.Drone == null)
          {
              return Problem("Entity set 'VergedbContext.Drone'  is null.");
          }
            _context.Drone.Add(drone); 
            await _context.SaveChangesAsync(); //pushes to database

            return CreatedAtAction("GetDrone", new { id = drone.DroneID }, drone); //201
        }

        // DELETE: api/Drones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDrone(int id)
        {
            if (_context.Drone == null)
            {
                return NotFound();
            }
            var drone = await _context.Drone.FindAsync(id);
            if (drone == null)
            {
                return NotFound();
            }

            _context.Drone.Remove(drone);
            await _context.SaveChangesAsync();

            return NoContent(); //204
        }

        private bool DroneExists(int id)
        {
            return (_context.Drone?.Any(e => e.DroneID == id)).GetValueOrDefault();
        }
    }
}