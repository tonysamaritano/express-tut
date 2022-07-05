using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using vergedb.Models;

namespace vergedb.Data
{
    public class VergedbContext : DbContext
    {
        public VergedbContext(DbContextOptions<VergedbContext> options)
            : base(options)
        {
        }

        //For multiple models need to update to have multiple sets
        public DbSet<Drone> Drone { get; set; } //Creates Drones table in Database
        public DbSet<Performance> Performance { get; set; } //Creates Drones table in Database

    }
}
