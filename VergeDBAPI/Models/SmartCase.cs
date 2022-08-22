using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VergeDBAPI.Models
{
    public class SmartCase
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("smartcase_id")]
        public int SmartCaseID { get; set; }
    }
}
