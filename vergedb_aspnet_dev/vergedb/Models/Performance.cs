using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace vergedb.Models
{
    public class Performance
    {
        [Key]
        [JsonProperty("key")]
        [JsonIgnore]
        public int PerformanceID { get; set; }
        [Required]
        [JsonProperty("slot")]
        public int Slot { get; set; }
        [Required]
        [JsonProperty("flight_time")]
        public float FlightTime { get; set; }
        [Required]
        [JsonProperty("start_battery")]
        public float StartingBattery { get; set; }
        [Required]
        [JsonProperty("end_battery")]
        public float EndingBattery { get; set; }

        //Dependencies on drone, many->one
        [JsonIgnore]
        public int DroneID { get; set; }
        [JsonIgnore]
        public virtual Drone? Drone { get; set; }
    }
}
