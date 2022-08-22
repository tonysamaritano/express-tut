using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VergeDBAPI.Models;
using VergeDBAPI.Validation;

namespace VergeDBAPI.Controllers
{
    [Route("v1/assets")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly VergeDBAPIContext _context;
        private UserData userData;

        public AssetsController(VergeDBAPIContext context)
        {
            _context = context;
            userData = new UserData();
        }

        // GET: v1/Assets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asset>>> GetAssets()
        {
            if (_context.Assets == null)
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

            if (userData.UserRole != OrgRole.Superuser)
            {
                return await _context.Assets.Where(a => a.Organization.Name == userData.UserOrg).ToListAsync();
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

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            var assets = await _context.Assets
                                .Where(i => i.AssetID == id)
                                .Include(o => o.Organization)
                                .FirstOrDefaultAsync();

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
                        owner = assets.Organization.Name,
                        metadata = new Drone
                        {
                            DroneUID = droneAsset.DroneUID,
                            FaaId = droneAsset.FaaId,
                            FlightHours = droneAsset.FlightHours,
                            Firmware = droneAsset.Firmware
                        }
                    });

                case AssetType.Battery:
                    var batteryAsset = await _context.Batteries.FindAsync(assets.TableKey);

                    return Ok(
                        new {
                        type_id = assets.TypeID.ToString(),
                        owner = assets.Organization.Name,
                        metadata = new Battery
                        {
                            BatteryID = batteryAsset.BatteryID,
                            BatteryCycles = batteryAsset.BatteryCycles,
                            BatteryType = batteryAsset.BatteryType
                        }
                    });
            }

            return NotFound("Asset not found");
        }

        // GET: v1/assets/{id}/performances
        [HttpGet("{id}/performances")]
        public async Task<ActionResult<IEnumerable<Asset>>> GetDronePerformances(int id)
        {
            if (_context.Assets == null)
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

            var asset = await _context.Assets.FindAsync(id);

            if(asset.TypeID == AssetType.Drone)
            {
                //return Problem($"Provided ID: {id} is not a drone, ID is {asset.TypeID.ToString()}");
                var droneAsset = await _context.Drones.FindAsync(asset.TableKey);

                var performances = await _context.Performances.Where(p => p.DroneID == droneAsset.DroneID).ToListAsync();
                return Ok(
                    new
                    {
                        drone_uid = droneAsset.DroneUID,
                        performances = performances
                    });
            }

            return Problem("Only drone performance implemented");
        }

        // PUT: v1/assets/reassign/5
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)},{nameof(OrgRole.Owner)}")]
        [HttpPut("reassign/{id}")]
        public async Task<IActionResult> ReassignOwner(int id, [FromForm] int newOrganization)
        {
            var asset = await _context.Assets.Where(i => i.AssetID == id).Include(o => o.Organization).FirstOrDefaultAsync();
            string oldOrg;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            if (asset == null)
            {
                return NotFound($"Asset {id} does not exist");
            }
            else
            {
                oldOrg = asset.Organization.Name;
                asset.OrganizationID = newOrganization;
                await _context.SaveChangesAsync();
            }

            return Ok($"Asset {id} reassigned from {oldOrg} to {newOrganization}");
        }

        // PUT: v1/assets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)},{nameof(OrgRole.Owner)}")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAssets(int id, Asset assets)
        {
            if (id != assets.AssetID)
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
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)},{nameof(OrgRole.Owner)}")]
        [HttpPost("drone")]
        public async Task<ActionResult<Asset>> PostAssets([FromForm] DroneForm drone)
        {
            if (_context.Assets == null)
            {
                return Problem("Entity set 'VergeDBAPIContext.Assets'  is null.");
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            var newAssetOrg = await _context.Organizations.FindAsync(drone.OrganizationID);

            if(!userData.UserOrg.Equals(newAssetOrg.Name) && userData.UserRole != OrgRole.Superuser)
            {
                return Problem("Rejected: user not eligible to post to organization");
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
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)},{nameof(OrgRole.Owner)}")]
        [HttpPost("battery")]
        public async Task<ActionResult<Asset>> PostAssets([FromForm] BatteryForm battery)
        {
            if (_context.Assets == null)
            {
                return Problem("Entity set 'VergeDBAPIContext.Assets'  is null.");
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            var newAssetOrg = await _context.Organizations.FindAsync(battery.OrganizationID);

            if (!userData.UserOrg.Equals(newAssetOrg.Name) && userData.UserRole != OrgRole.Superuser)
            {
                return Problem("Rejected: user not eligible to post to organization");
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
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)},{nameof(OrgRole.Owner)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssets(int id)
        {
            if (_context.Assets == null)
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
