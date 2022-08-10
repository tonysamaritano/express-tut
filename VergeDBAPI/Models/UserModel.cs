using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace VergeDBAPI.Models
{
    public class UserModel
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("UserModelID")]
        public int UserModelID { get; set; }
        [Required]
        [JsonProperty("Username")]
        public string? Username { get; set; }
        [Required]
        [JsonProperty("Password")]
        public string? Password { get; set; }
        [Required]
        [JsonProperty("Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [Required]
        [JsonProperty("Company")]
        [StringLength(20)]
        public string? Company { get; set; }
        [Required]
        [JsonProperty("Role")]
        [StringLength(20)]
        public string? Role { get; set; }
        
    }
}
