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

        public DbSet<Drone> Drone { get; set; } //Creates Drones table in Database
        public DbSet<Performance> Performance { get; set; } //Creates Drones table in Database
        public DbSet<vergedb.Models.Owner>? Owner { get; set; }
        public DbSet<UserModel> User { get; set; }

    }
}
