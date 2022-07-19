using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace vergedb.Models
{
    [Keyless]
    public class UserModel
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Company { get; set; }
        public string? Role { get; set; }
    }
}
