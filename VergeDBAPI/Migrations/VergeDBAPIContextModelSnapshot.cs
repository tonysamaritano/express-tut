﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VergeDBAPI;

#nullable disable

namespace VergeDBAPI.Migrations
{
    [DbContext(typeof(VergeDBAPIContext))]
    partial class VergeDBAPIContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("VergeDBAPI.Models.Asset", b =>
                {
                    b.Property<int>("AssetID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssetID"), 1L, 1);

                    b.Property<short>("OrganizationID")
                        .HasColumnType("smallint");

                    b.Property<int>("TableKey")
                        .HasColumnType("int");

                    b.Property<decimal>("TypeID")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("AssetID");

                    b.HasIndex("TypeID", "TableKey")
                        .IsUnique();

                    b.ToTable("Assets");

                    b.HasData(
                        new
                        {
                            AssetID = 1,
                            OrganizationID = (short)-1,
                            TableKey = 3,
                            TypeID = 0m
                        },
                        new
                        {
                            AssetID = 2,
                            OrganizationID = (short)2,
                            TableKey = 1,
                            TypeID = 0m
                        },
                        new
                        {
                            AssetID = 3,
                            OrganizationID = (short)0,
                            TableKey = 4,
                            TypeID = 0m
                        },
                        new
                        {
                            AssetID = 4,
                            OrganizationID = (short)-1,
                            TableKey = 2,
                            TypeID = 0m
                        },
                        new
                        {
                            AssetID = 5,
                            OrganizationID = (short)1,
                            TableKey = 3,
                            TypeID = 1m
                        },
                        new
                        {
                            AssetID = 6,
                            OrganizationID = (short)1,
                            TableKey = 1,
                            TypeID = 1m
                        },
                        new
                        {
                            AssetID = 7,
                            OrganizationID = (short)0,
                            TableKey = 2,
                            TypeID = 1m
                        });
                });

            modelBuilder.Entity("VergeDBAPI.Models.Battery", b =>
                {
                    b.Property<int>("BatteryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BatteryID"), 1L, 1);

                    b.Property<int>("BatteryCycles")
                        .HasColumnType("int");

                    b.Property<int>("BatteryType")
                        .HasColumnType("int");

                    b.HasKey("BatteryID");

                    b.ToTable("Batteries");

                    b.HasData(
                        new
                        {
                            BatteryID = 1,
                            BatteryCycles = 20,
                            BatteryType = 0
                        },
                        new
                        {
                            BatteryID = 2,
                            BatteryCycles = 331,
                            BatteryType = 1
                        },
                        new
                        {
                            BatteryID = 3,
                            BatteryCycles = 0,
                            BatteryType = 0
                        });
                });

            modelBuilder.Entity("VergeDBAPI.Models.Drone", b =>
                {
                    b.Property<int>("DroneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DroneID"), 1L, 1);

                    b.Property<int>("DroneUID")
                        .HasColumnType("int");

                    b.Property<string>("FaaId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Firmware")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("FlightHours")
                        .HasColumnType("int");

                    b.HasKey("DroneID");

                    b.HasIndex("DroneUID")
                        .IsUnique();

                    b.HasIndex("FaaId")
                        .IsUnique()
                        .HasFilter("[FaaId] IS NOT NULL");

                    b.ToTable("Drone");

                    b.HasData(
                        new
                        {
                            DroneID = 1,
                            DroneUID = 1001,
                            FaaId = "GTF43UH3S5",
                            Firmware = "1.1.1.3",
                            FlightHours = 2000
                        },
                        new
                        {
                            DroneID = 2,
                            DroneUID = 392,
                            FaaId = "NM7A0B1P2S",
                            Firmware = "",
                            FlightHours = 191
                        },
                        new
                        {
                            DroneID = 3,
                            DroneUID = 2312,
                            Firmware = "",
                            FlightHours = 600
                        },
                        new
                        {
                            DroneID = 4,
                            DroneUID = 912,
                            Firmware = "",
                            FlightHours = 0
                        });
                });

            modelBuilder.Entity("VergeDBAPI.Models.UserModel", b =>
                {
                    b.Property<int>("UserModelID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserModelID"), 1L, 1);

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserModelID");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            UserModelID = 1,
                            Company = "Verge Aero",
                            Email = "Tony@vergeaero.com",
                            Password = "Drones",
                            Role = "Wizardry",
                            Username = "Tony"
                        },
                        new
                        {
                            UserModelID = 2,
                            Company = "Splurge Aero",
                            Email = "Gabe@gabe.gov",
                            Password = "Gabe",
                            Role = "Drone God",
                            Username = "Gabe"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
