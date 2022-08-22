using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VergeDBAPI.Models;
using VergeDBAPI.Validation;

namespace VergeDBAPI.Controllers
{
    [Route("v1/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly VergeDBAPIContext _context;
        private UserData userData;

        public UserController(VergeDBAPIContext context)
        {
            _context = context;
            userData = new UserData();
        }

        // POST: v1/user/register
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)}")]
        [HttpPost("/register")]
        public async Task<ActionResult<User>> Register([FromForm] User newUser)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = newUser.UserID }, newUser);
        }

        // PUT: v1/user/role/5
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)},{nameof(OrgRole.Admin)},{nameof(OrgRole.Owner)}")]
        [HttpPut("role/{id}")]
        public async Task<IActionResult> ChangeRole(int id, [FromForm] OrgRole newRole)
        {
            var user = await _context.Users.Where(i => i.UserID == id).Include(m => m.Membership).FirstOrDefaultAsync();
            OrgRole oldRole;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            if(ValidateOrganization(user.Membership.Organization.Name))
            {
                return Problem("Access denied to external company");
            }

            if (user == null)
            {
                return NotFound($"User {id} does not exist");
            }
            else
            {
                oldRole = user.Membership.Role;
                user.Membership.Role = newRole;
                await _context.SaveChangesAsync();
            }

            return Ok($"User {id} reassigned from {oldRole} to {newRole}");
        }

        // DELETE: v1/user/5
        [Authorize(Roles = $"{nameof(OrgRole.Superuser)}")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            userData.Identify(identity);
            var validUser = UserValidation.Validate(_context, userData).Result;

            if (validUser != null)
            {
                return Problem(validUser);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UserID == id)).GetValueOrDefault();
        }

        private bool ValidateOrganization(string company)
        {
            return (userData.UserOrg != company) && (userData.UserRole != OrgRole.Superuser);
        }
    }
}
