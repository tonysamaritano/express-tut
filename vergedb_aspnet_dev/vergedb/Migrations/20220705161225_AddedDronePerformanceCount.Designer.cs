﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vergedb.Data;

#nullable disable

namespace vergedb.Migrations
{
    [DbContext(typeof(VergedbContext))]
    [Migration("20220705161225_AddedDronePerformanceCount")]
    partial class AddedDronePerformanceCount
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("vergedb.Models.Drone", b =>
                {
                    b.Property<int>("DroneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DroneID"), 1L, 1);

                    b.Property<int>("DroneUID")
                        .HasColumnType("int");

                    b.Property<string>("FaaId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PerformanceCount")
                        .HasColumnType("int");

                    b.Property<string>("PixHardware")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DroneID");

                    b.ToTable("Drone");
                });

            modelBuilder.Entity("vergedb.Models.Performance", b =>
                {
                    b.Property<int>("PerformanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PerformanceID"), 1L, 1);

                    b.Property<int>("DroneID")
                        .HasColumnType("int");

                    b.Property<float>("EndingBattery")
                        .HasColumnType("real");

                    b.Property<float>("FlightTime")
                        .HasColumnType("real");

                    b.Property<int>("Slot")
                        .HasColumnType("int");

                    b.Property<float>("StartingBattery")
                        .HasColumnType("real");

                    b.HasKey("PerformanceID");

                    b.HasIndex("DroneID");

                    b.ToTable("Performance");
                });

            modelBuilder.Entity("vergedb.Models.Performance", b =>
                {
                    b.HasOne("vergedb.Models.Drone", "Drone")
                        .WithMany("Performances")
                        .HasForeignKey("DroneID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drone");
                });

            modelBuilder.Entity("vergedb.Models.Drone", b =>
                {
                    b.Navigation("Performances");
                });
#pragma warning restore 612, 618
        }
    }
}
