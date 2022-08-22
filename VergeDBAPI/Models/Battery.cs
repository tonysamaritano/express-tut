using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VergeDBAPI.Models
{
    public class Battery
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("battery_id")]
        public int BatteryID { get; set; }
        [JsonProperty("battery_cycles")]
        public int BatteryCycles { get; set; } = 0;
        [Required(ErrorMessage = "Battery type required")]
        [JsonProperty("battery_type")]
        public int BatteryType { get; set; }
    }

    public class BatteryForm : Battery
    {
        [Required]
        public int OrganizationID { get; set; }
    }
}
