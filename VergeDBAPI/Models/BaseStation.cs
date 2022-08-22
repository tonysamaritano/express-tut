using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VergeDBAPI.Models
{
    public class BaseStation
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("basestation_id")]
        public int BaseStationID { get; set; }
    }
}
