using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VergeDBAPI.Models
{
    public class User
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("user_model_id")]
        public int UserID { get; set; }
        [Required]
        [JsonProperty("username")]
        public string? Username { get; set; }
        [Required]
        [JsonProperty("password")]
        public string? Password { get; set; }
        [Required]
        [JsonProperty("email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string? Email { get; set; }
        [JsonIgnore]
        public OrganizationMembership Membership { get; set; }
    }
}