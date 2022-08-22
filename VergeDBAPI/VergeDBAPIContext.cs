using Microsoft.EntityFrameworkCore;
using VergeDBAPI.Models;

namespace VergeDBAPI
{
    public class VergeDBAPIContext : DbContext
    {
        public VergeDBAPIContext(DbContextOptions<VergeDBAPIContext> options)
            : base(options)
        {
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Drone> Drones { get; set; }
        public DbSet<Battery> Batteries { get; set; }
        public DbSet<BaseStation> BaseStations { get; set; }
        public DbSet<Gateway> Gateways { get; set; }
        public DbSet<SmartCase> SmartCases { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationMembership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Force unique properties
            modelBuilder.Entity<Drone>()
                .HasIndex(u => u.DroneUID)
                .IsUnique();

            modelBuilder.Entity<Drone>()
                .HasIndex(u => u.FaaId)
                .IsUnique();

            //Only 1 asset entry per table key
            modelBuilder.Entity<Asset>()
                .HasIndex(u => new { u.TypeID, u.TableKey })
                .IsUnique();

            modelBuilder.Entity<OrganizationMembership>()
                .HasOne(u => u.User)
                .WithOne(m => m.Membership)
                .OnDelete(DeleteBehavior.Restrict);

            var drones = new List<Drone>()
            {
                new Drone
                {
                    DroneID = 1,
                    DroneUID = 1001,
                    FaaId = "GTF43UH3S5",
                    FlightHours = 2000,
                    Firmware = "1.1.1.3"
                },
                new Drone
                {
                    DroneID = 2,
                    DroneUID = 392,
                    FaaId = "NM7A0B1P2S",
                    FlightHours = 191
                },
                new Drone
                {
                    DroneID = 3,
                    DroneUID = 2312,
                    FlightHours = 600
                },
                new Drone
                {
                    DroneID = 4,
                    DroneUID = 912
                }
            };
            drones.ForEach(d => modelBuilder.Entity<Drone>().HasData(d));

            var performances = new List<Performance>
            {
                new Performance
                {
                    PerformanceID = 1,
                    Slot = 188,
                    FlightTime = 10,
                    StartingBattery = 12.6f,
                    EndingBattery = 11.6f,
                    Date = DateTime.Parse("2022-7-29"),
                    DroneID = drones.Single(d => d.DroneUID == 392).DroneID
                },
                new Performance
                {
                    PerformanceID = 2,
                    Slot = 23,
                    FlightTime = 10.03f,
                    StartingBattery = 12.6f,
                    EndingBattery = 11.4f,
                    Date = DateTime.Parse("2022-7-29"),
                    DroneID = drones.Single(d => d.DroneUID == 912).DroneID
                },
                new Performance
                {
                    PerformanceID = 3,
                    Slot = 26,
                    FlightTime = 2,
                    StartingBattery = 12.6f,
                    EndingBattery = 12.4f,
                    Date = DateTime.Parse("2022-8-19"),
                    DroneID = drones.Single(d => d.DroneUID == 392).DroneID
                }
            };
            performances.ForEach(p => modelBuilder.Entity<Performance>().HasData(p));
           
            var batteries = new List<Battery>
            {
                new Battery
                {
                    BatteryID = 1,
                    BatteryCycles = 20,
                    BatteryType = 0
                },
                new Battery
                {
                    BatteryID = 2,
                    BatteryCycles = 331,
                    BatteryType = 1
                },
                new Battery
                {
                    BatteryID = 3,
                    BatteryType = 0
                }
            };
            batteries.ForEach(b => modelBuilder.Entity<Battery>().HasData(b));

            var baseStations = new List<BaseStation>
            {
                new BaseStation
                {
                    BaseStationID = 1
                }
            };
            baseStations.ForEach(bs => modelBuilder.Entity<BaseStation>().HasData(bs));

            var gateways = new List<Gateway>
            {
                new Gateway
                {
                    GatewayID = 1
                }
            };
            gateways.ForEach(gw => modelBuilder.Entity<Gateway>().HasData(gw));

            var smartCases = new List<SmartCase>
            {
                new SmartCase
                {
                    SmartCaseID = 1
                }
            };
            smartCases.ForEach(sc => modelBuilder.Entity<SmartCase>().HasData(sc));

            var users = new List<User>
            {
                new User
                {
                    UserID = 1,
                    Username = "Tony",
                    Password = "Drones",
                    Email = "Tony@vergeaero.com"
                },
                new User
                {
                    UserID = 2,
                    Username = "Gabe",
                    Password = "Gabe",
                    Email = "Gabe@gabe.gov"
                },
                new User
                {
                    UserID = 3,
                    Username = "Ronald",
                    Password = "Donald",
                    Email = "ronalddonald@mcd.gov"
                },
                new User
                {
                    UserID = 4,
                    Username = "Jeremy",
                    Password = "j123",
                    Email = "jeremy@strictly.net"
                },
                new User
                {
                    UserID = 5,
                    Username = "John",
                    Password = "johnny",
                    Email = "johnny@go.net"
                }
            };
            users.ForEach(u => modelBuilder.Entity<User>().HasData(u));

            var orgs = new List<Organization>
            {
                new Organization
                {
                    ID = 1,
                    OwnerID = users.Single(u => u.Username == "Tony").UserID,
                    Name = "Verge Aero",
                    Address = "7905 Browning Road, Pensauken, NJ",
                    OrganizationMetadata = ""
                },
                new Organization
                {
                    ID = 2,
                    OwnerID = users.Single(u => u.Username == "Jeremy").UserID,
                    Name = "Strictly",
                    Address = "756 S. Glasgow Avenue Inglewood, CA 90301",
                    OrganizationMetadata = ""
                },
                new Organization
                {
                    ID = 3,
                    OwnerID = users.Single(u => u.Username == "John").UserID,
                    Name = "Go Agency",
                    Address = "1144 Tampa Road, Palm Harbor, FL 34683",
                    OrganizationMetadata = ""
                }
            };
            orgs.ForEach(o => modelBuilder.Entity<Organization>().HasData(o));

            var memberships = new List<OrganizationMembership>
            {
                new OrganizationMembership
                {
                    ID = 1,
                    Role = OrgRole.Superuser,
                    Position = "Engineer",
                    UserID = users.Single(u => u.Username == "Tony").UserID,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new OrganizationMembership
                {
                    ID = 2,
                    Role = OrgRole.User,
                    Position = "Intern",
                    UserID = users.Single(u => u.Username == "Gabe").UserID,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new OrganizationMembership
                {
                    ID = 3,
                    Role = OrgRole.Banned,
                    Position = "Sales",
                    UserID = users.Single(u => u.Username == "Ronald").UserID,
                    OrganizationID = orgs.Single(o => o.Name == "Strictly").ID
                },
                new OrganizationMembership
                {
                    ID = 4,
                    Role = OrgRole.Owner,
                    Position = "CEO",
                    UserID = users.Single(u => u.Username == "Jeremy").UserID,
                    OrganizationID = orgs.Single(o => o.Name == "Strictly").ID
                },
                new OrganizationMembership
                {
                    ID = 5,
                    Role = OrgRole.Owner,
                    Position = "CEO",
                    UserID = users.Single(u => u.Username == "John").UserID,
                    OrganizationID = orgs.Single(o => o.Name == "Go Agency").ID
                }
            };
            memberships.ForEach(m => modelBuilder.Entity<OrganizationMembership>().HasData(m));

            var assets = new List<Asset>
            {
                new Asset
                {
                    AssetID = 1,
                    TypeID = AssetType.Drone,
                    TableKey = drones.Single(d => d.DroneUID == 2312).DroneID,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new Asset
                {
                    AssetID = 2,
                    TypeID = AssetType.Drone,
                    TableKey = 1,
                    OrganizationID = orgs.Single(o => o.Name == "Go Agency").ID
                },
                new Asset
                {
                    AssetID = 3,
                    TypeID = AssetType.Drone,
                    TableKey = 4,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new Asset
                {
                    AssetID = 4,
                    TypeID = AssetType.Drone,
                    TableKey = 2,
                    OrganizationID = orgs.Single(o => o.Name == "Strictly").ID
                },
                new Asset
                {
                    AssetID = 5,
                    TypeID = AssetType.Battery,
                    TableKey = 3,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new Asset
                {
                    AssetID = 6,
                    TypeID = AssetType.Battery,
                    TableKey = 1,
                    OrganizationID = orgs.Single(o => o.Name == "Go Agency").ID
                },
                new Asset
                {
                    AssetID = 7,
                    TypeID = AssetType.Battery,
                    TableKey = 2,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new Asset
                {
                    AssetID = 8,
                    TypeID = AssetType.BaseStation,
                    TableKey = 1,
                    OrganizationID = orgs.Single(o => o.Name == "Verge Aero").ID
                },
                new Asset
                {
                    AssetID = 9,
                    TypeID = AssetType.Gateway,
                    TableKey = 1,
                    OrganizationID = orgs.Single(o => o.Name == "Go Agency").ID
                },
                new Asset
                {
                    AssetID = 10,
                    TypeID = AssetType.SmartCase,
                    TableKey = 1,
                    OrganizationID = orgs.Single(o => o.Name == "Strictly").ID
                }
            };
            assets.ForEach(a => modelBuilder.Entity<Asset>().HasData(a));
        }
    }
}