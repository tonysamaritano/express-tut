using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
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
        //public DbSet<UserLogin> UserLogin { get; set; }
        public DbSet<UserModel> User { get; set; }
        public DbSet<Drone> Drone { get; set; } //Creates Drones table in Database
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

            //Drones
            modelBuilder.Entity<Drone>().HasData(new Drone
            {
                DroneID = 1,
                DroneUID = 1001,
                FaaId = "GTF43UH3S5",
                FlightHours = 2000,
                Firmware = "1.1.1.3"
            });

            modelBuilder.Entity<Drone>().HasData(new Drone
            {
                DroneID = 2,
                DroneUID = 392,
                FaaId = "NM7A0B1P2S",
                FlightHours = 191
            });

            modelBuilder.Entity<Drone>().HasData(new Drone
            {
                DroneID = 3,
                DroneUID = 2312,
                FlightHours = 600
            });

            modelBuilder.Entity<Drone>().HasData(new Drone
            {
                DroneID = 4,
                DroneUID = 912
            });

            //Batteries
            modelBuilder.Entity<Battery>().HasData(new Battery
            {
                BatteryID = 1,
                BatteryCycles = 20,
                BatteryType = 0
            });

            modelBuilder.Entity<Battery>().HasData(new Battery
            {
                BatteryID = 2,
                BatteryCycles = 331,
                BatteryType = 1
            });

            modelBuilder.Entity<Battery>().HasData(new Battery
            {
                BatteryID = 3,
                BatteryType = 0
            });

            //Assets
            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 1,
                TypeID = AssetType.Drone,
                TableKey = 3,
                OrganizationID = OrganizationId.Unowned
            });

            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 2,
                TypeID = AssetType.Drone,
                TableKey = 1,
                OrganizationID = OrganizationId.Go
            });

            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 3,
                TypeID = AssetType.Drone,
                TableKey = 4,
                OrganizationID = OrganizationId.Verge
            });

            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 4,
                TypeID = AssetType.Drone,
                TableKey = 2,
                OrganizationID = OrganizationId.Unowned
            });

            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 5,
                TypeID = AssetType.Battery,
                TableKey = 3,
                OrganizationID = OrganizationId.Strictly
            });

            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 6,
                TypeID = AssetType.Battery,
                TableKey = 1,
                OrganizationID = OrganizationId.Strictly
            });

            modelBuilder.Entity<Asset>().HasData(new Asset
            {
                AssetID = 7,
                TypeID = AssetType.Battery,
                TableKey = 2,
                OrganizationID = OrganizationId.Verge
            });
     
            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                UserModelID = 1,
                Username = "Tony",
                Password = "Drones",
                Email = "Tony@vergeaero.com",
                Company = "Verge Aero",
                Role = "Wizardry"
            });

            modelBuilder.Entity<UserModel>().HasData(new UserModel
            {
                UserModelID = 2,
                Username = "Gabe",
                Password = "Gabe",
                Email = "Gabe@gabe.gov",
                Company = "Splurge Aero",
                Role = "Drone God"
            });

        }
    }
}