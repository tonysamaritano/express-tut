using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace vergedb.Models
{
    public class Owner
    {
        [Key]
        [JsonProperty("key")]
        public int OwnerID { get; set; }
        [Required]
        [JsonProperty("company_name")]
        public string CompanyName { get; set; }
        [Required]
        [JsonProperty("number_drones")]
        public int NumDrones { get; set; }

        [JsonProperty("drones")]
        public virtual ICollection<Drone>? Drones { get; set; }
    }
}
