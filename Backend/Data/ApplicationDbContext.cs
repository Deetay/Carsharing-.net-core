using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Ride> Rides { get; set; }
        public DbSet<PassengerRide> PassengerRides { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PassengerRide>()
                .HasKey(pr => new { pr.UserId, pr.RideId });
            modelBuilder.Entity<PassengerRide>()
                .HasOne(pr => pr.User)
                .WithMany(pr => pr.PassengerRides)
                .HasForeignKey(pr => pr.UserId);
            modelBuilder.Entity<PassengerRide>()
                .HasOne(pr => pr.Ride)
                .WithMany(pr => pr.PassengerRides)
                .HasForeignKey(pr => pr.RideId);
        }
    }
}
