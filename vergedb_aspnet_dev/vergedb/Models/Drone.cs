using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace vergedb.Models
{
    public class Drone
    {
        [Key]
        [JsonProperty("key")]
        public int DroneID { get; set; }
        [Required]
        [JsonProperty("droneUID")]
        public int DroneUID { get; set; }
        [Required]
        [JsonProperty("faa_id")]
        public string FaaId { get; set; }
        [Required]
        [JsonProperty("cube_version")]
        [RegularExpression(@"(orange|black)", ErrorMessage = "Specify [orange] or [black] cube.")]
        public string PixHardware { get; set; }
        [Required]
        [JsonProperty("performance_count")]
        public int PerformanceCount { get; set; }


        //Definitions for performance tables, one->many
        [JsonProperty("performances")]
        public virtual ICollection<Performance>? Performances { get; set; }

        //many->one
        //[JsonIgnore]
        [JsonIgnore]
        public virtual Owner? Owner { get; set; }
        [JsonProperty("owner_name")]
        public string OwnerName { get; set; }
    }
}
