using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using VergeDBAPI.Models;

namespace VergeDBAPI.Controllers
{
    [Route("v1/assets")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly VergeDBAPIContext _context;

        public AssetsController(VergeDBAPIContext context)
        {
            _context = context;
        }

        // GET: v1/Assets
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }

            return await _context.Assets.ToListAsync();
        }

        // GET: v1/Assets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asset>> GetAssets(int id)
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }
            var assets = await _context.Assets.FindAsync(id);

            if (assets == null)
            {
                return NotFound();
            }

            switch (assets.TypeID)
            {
                case AssetType.Drone:
                    var droneAsset = await _context.Drones.FindAsync(assets.TableKey);

                    return Ok(
                        new {
                        type_id = assets.TypeID.ToString(),
                        metadata = new Drone
                        {
                            DroneUID = droneAsset.DroneUID,
                            FaaId = droneAsset.FaaId,
                            FlightHours = droneAsset.FlightHours,
                            Firmware = droneAsset.Firmware
                        },
                        organization_id = assets.OrganizationID.ToString()   // testing
                    });

                case AssetType.Battery:
                    var batteryAsset = await _context.Batteries.FindAsync(assets.TableKey);

                    return Ok(
                        new {
                        type_id = assets.TypeID.ToString(),
                        metadata = new Battery
                        {
                            BatteryID = batteryAsset.BatteryID,
                            BatteryCycles = batteryAsset.BatteryCycles,
                            BatteryType = batteryAsset.BatteryType
                        },
                        organization_id = assets.OrganizationID.ToString()   // testing
                    });
            }

            return NotFound("Asset not found");
        }

        // PUT: v1/assets/reassign/5
        [HttpPut("reassign/{id}")]
        public async Task<IActionResult> ReassignOwner(int id, [FromForm] OrganizationId newOrganization)
        {
            var asset = await _context.Assets.Where(i => i.AssetID == id).FirstOrDefaultAsync();
            OrganizationId oldOrg;

            if (asset == null)
            {
                return NotFound($"Asset {id} does not exist");
            }
            else
            {
                oldOrg = asset.OrganizationID;
                asset.OrganizationID = newOrganization;
                await _context.SaveChangesAsync();
            }

            return Ok($"Asset {id} reassigned from {oldOrg} to {newOrganization}");
        }

        // PUT: v1/assets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssets(int id, Asset assets)
        {
            if (id != assets.AssetID)
            {
                return BadRequest();
            }

            _context.Entry(assets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetsExists(id))
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

        // POST: v1/Assets/Drone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("drone")]
        public async Task<ActionResult<Asset>> PostAssets([FromForm] DroneForm drone)
        {
            if (_context.Assets == null)
            {
                return Problem("Entity set 'VergeDBAPIContext.Assets'  is null.");
            }

            Drone newDrone = new Drone()
            {
                DroneUID = drone.DroneUID,
                FaaId = drone.FaaId,
                FlightHours = drone.FlightHours,
                Firmware = drone.Firmware
            };

            _context.Drones.Add(newDrone);
            await _context.SaveChangesAsync();
            _context.Entry(newDrone).GetDatabaseValues();

            //Would rather have a way to get new PK immediately without saving
            Asset newAsset = new Asset()
            {
                TypeID = AssetType.Drone,
                TableKey = newDrone.DroneID,
                OrganizationID = drone.OrganizationID
            };

            _context.Assets.Add(newAsset);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssets", new { id = newAsset.AssetID }, newAsset);
        }

        // POST: v1/Assets/battery
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("battery")]
        public async Task<ActionResult<Asset>> PostAssets([FromForm] BatteryForm battery)
        {
            if (_context.Assets == null)
            {
                return Problem("Entity set 'VergeDBAPIContext.Assets'  is null.");
            }

            Battery newBattery = new Battery()
            {
                BatteryCycles = battery.BatteryCycles,
                BatteryType = battery.BatteryType,
            };
            _context.Batteries.Add(newBattery);
            await _context.SaveChangesAsync();
            _context.Entry(newBattery).GetDatabaseValues();

            //Would rather have a way to get new PK immediately without saving

            Asset newAsset = new Asset()
            {
                TypeID = AssetType.Battery,
                TableKey = newBattery.BatteryID,
                OrganizationID = battery.OrganizationID
            };
            
            _context.Assets.Add(newAsset);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAssets", new { id = newAsset.AssetID }, newAsset);
        }

        // DELETE: v1/Assets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssets(int id)
        {
            if (_context.Assets == null)
            {
                return NotFound();
            }
            var assets = await _context.Assets.FindAsync(id);
            if (assets == null)
            {
                return NotFound();
            }

            _context.Assets.Remove(assets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AssetsExists(int id)
        {
            return (_context.Assets?.Any(e => e.AssetID == id)).GetValueOrDefault();
        }
    }
}
