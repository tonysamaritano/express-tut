using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using VergeDBAPI.Models;

namespace VergeDBAPI.Models
{
    public class OrganizationMetadata
    {
        public bool InstantAuthPermission { get; set; }
    }
    
    public class OrganizationInfo
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public OrganizationMetadata Metadata { get; set; }
    }
    
    public class Organization
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? OwnerID { get; set; }
        [ForeignKey("OwnerID"), System.Text.Json.Serialization.JsonIgnore]
        public User Owner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public string OrganizationMetadata { get; set; }
        [NotMapped]
        public OrganizationMetadata OrgMetadata
        {
            get
            {
                if (string.IsNullOrEmpty(OrganizationMetadata))
                    return new OrganizationMetadata();
                return JsonConvert.DeserializeObject<OrganizationMetadata>(OrganizationMetadata);
            }
            set => OrganizationMetadata = JsonConvert.SerializeObject(value);
        }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public ICollection<OrganizationMembership> UserMemberships { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public ICollection<FlightAuthorizationData> FlightAuthorizations { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public ICollection<Subscription> Subscriptions { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public ICollection<Show> Shows { get; set; }
        //​
        public OrganizationInfo GetInfo()
        {
            return new OrganizationInfo()
            {
                Address = Address,
                Name = Name,
                Metadata = OrgMetadata
            };
        }
    }
    
    public enum OrgRole
    {
        Banned = -1,
        None,
        Guest,
        User,
        Manager,
        Admin,
        Owner,
        Superuser
    }
    
    public class OrganizationMembership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary>
        /// Role of the user in this organization
        /// </summary>
        public OrgRole Role { get; set; }
        public string? Position { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        public int OrganizationID { get; set; }
        /// <summary>
        /// Organization that this role belongs to
        /// </summary>
        [ForeignKey("OrganizationID")]
        public Organization? Organization { get; set; }
    }
}