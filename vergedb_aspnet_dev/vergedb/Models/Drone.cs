using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;
//using System.Text.Json.Serialization;

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

        //Definitions for performance tables, one->many
        //[DisplayFormat(NullDisplayText = "No Performances")]
        [JsonProperty("performances")]
        public virtual ICollection<Performance>? Performances { get; set; }
    }
}
