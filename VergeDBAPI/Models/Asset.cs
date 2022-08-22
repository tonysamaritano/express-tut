using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VergeDBAPI.Models
{
    public class Asset
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("asset_id")]
        public int AssetID { get; set; }
        [Required]
        [JsonProperty("type_id")]
        public AssetType TypeID { get; set; }
        [Required(ErrorMessage = "Related table key for asset information required")]
        [JsonProperty("table_key")]
        public int TableKey { get; set; }
        [JsonProperty("organization_id")]
        public int OrganizationID { get; set; }
        [ForeignKey("OrganizationID")]
        [JsonIgnore]
        public Organization Organization { get; set; }
    }

    [Flags]
    public enum AssetType : ulong
    {
        Drone = 0,
        Battery = 1,
        BaseStation = 2,
        Gateway = 4,
        SmartCase = 8
    }
}