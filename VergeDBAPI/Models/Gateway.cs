using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VergeDBAPI.Models
{
    public class Gateway
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("gateway_id")]
        public int GatewayID { get; set; }
    }
}
