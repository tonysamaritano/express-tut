using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VergeDBAPI.Models
{
    public class Drone
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("drone_id")]
        public int DroneID { get; set; }
        [Required]
        [JsonProperty("drone_uid")]
        public int DroneUID { get; set; }
        [JsonProperty("faa_id")]
        [RegularExpression(@"^[a-zA-Z]{10}$", ErrorMessage = "Must be 10 characters.")]
        public string? FaaId { get; set; } = null;
        [JsonProperty("flight_hours")]
        public int FlightHours { get; set; } = 0;
        [JsonProperty("firmware")]
        [RegularExpression(@"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$", ErrorMessage = "Please format as X.X.X.X")]
        public string Firmware { get; set; } = "";

        [JsonProperty("performances")]
        [JsonIgnore]
        public ICollection<Performance>? Performances { get; set; }
    }

    public class DroneForm : Drone
    {
        //Type ID and Table key can be set in controller
        [Required]
        public int OrganizationID { get; set; }
    }
}