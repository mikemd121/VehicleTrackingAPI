using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracking.Data.EntityModels;

namespace VehicleTrackingAPI.Models
{
    public class VehicleTrackContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTrackContext"/> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public VehicleTrackContext(DbContextOptions options) : base(options) { }

        /// <summary>
        /// Gets or sets the location trackers.
        /// </summary>

        public DbSet<LocationTracker> LocationTrackers { get; set; }

        /// <summary>
        /// Gets or sets the vehicles.
        /// </summary>
        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .HasData(
               new User {  UserName = "admin", Password = "1234" },
               new User {  UserName = "admin2", Password = "12345" });
        }
    }
}