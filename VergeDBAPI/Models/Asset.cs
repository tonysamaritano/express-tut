using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace VergeDBAPI.Models
{
    public class Asset
    {
        [Key]
        [JsonIgnore]
        [JsonProperty("asset_id")]
        public int AssetID { get; set; }
        [JsonIgnore]
        [Required]
        [JsonProperty("type_id")]
        public AssetType TypeID { get; set; }
        [Required(ErrorMessage = "Related table key for asset information required")]
        [JsonProperty("table_key")]
        public int TableKey { get; set; }
        [JsonProperty("organization_id")]
        public OrganizationId OrganizationID { get; set; } = OrganizationId.Unowned;
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

    //Temporary organization management
    public enum OrganizationId : short
    {
        Unowned = -1,
        Verge = 0,
        Strictly = 1,
        Go = 2
    }
}