using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VergeDBAPI.Models
{
    public class Performance
    {
        [Key]
        [JsonProperty("performance_id")]
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
        [Required]
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        //Dependencies on drone, many->one
        [JsonIgnore]
        public int DroneID { get; set; }
        [JsonIgnore]
        public Drone? Drone { get; set; }
    }

    public class PerformanceForm : Performance
    {
        [Required]
        [JsonProperty("drone_uid")]
        public int DroneUID { get; set; }
    }
}
