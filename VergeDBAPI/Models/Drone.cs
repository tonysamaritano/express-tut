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
        [StringLength(10)]
        public string? FaaId { get; set; } = null;
        [JsonProperty("flight_hours")]
        public int FlightHours { get; set; } = 0;
        [JsonProperty("firmware")]
        [StringLength(50)]
        public string Firmware { get; set; } = "";
    }

    public class DroneForm : Drone
    {
        //Type ID and Table key can be set in controller
        public OrganizationId OrganizationID { get; set; } = OrganizationId.Unowned;
    }
}