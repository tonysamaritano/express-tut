using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
//using System.Text.Json.Serialization;
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

        //Dependencies on drone, many->one
        [JsonIgnore]
        public int DroneID { get; set; }
        [JsonIgnore]
        public virtual Drone? Drone { get; set; }
    }
}
