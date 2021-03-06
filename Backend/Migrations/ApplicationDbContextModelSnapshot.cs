﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085");

            modelBuilder.Entity("Backend.Models.ApplicationUser", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("Car");

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Phone");

                    b.Property<byte[]>("Photo");

                    b.Property<long>("RateAmount");

                    b.Property<double>("Rating");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Backend.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("County");

                    b.Property<string>("Name");

                    b.Property<int>("Population");

                    b.Property<string>("Province");

                    b.HasKey("CityId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Backend.Models.PassengerRide", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RideId");

                    b.HasKey("UserId", "RideId");

                    b.HasIndex("RideId");

                    b.ToTable("PassengerRides");
                });

            modelBuilder.Entity("Backend.Models.Place", b =>
                {
                    b.Property<int>("PlaceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CityId");

                    b.Property<string>("PlaceInfo");

                    b.HasKey("PlaceId");

                    b.HasIndex("CityId");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("Backend.Models.Ride", b =>
                {
                    b.Property<int>("RideId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BookedSeats");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("FromPlaceId");

                    b.Property<int>("NumOfSeats");

                    b.Property<int>("OwnerId");

                    b.Property<double>("Price");

                    b.Property<int?>("ToPlaceId");

                    b.HasKey("RideId");

                    b.HasIndex("FromPlaceId");

                    b.HasIndex("ToPlaceId");

                    b.ToTable("Ride");
                });

            modelBuilder.Entity("Backend.Models.PassengerRide", b =>
                {
                    b.HasOne("Backend.Models.Ride", "Ride")
                        .WithMany("PassengerRides")
                        .HasForeignKey("RideId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Backend.Models.ApplicationUser", "User")
                        .WithMany("PassengerRides")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Backend.Models.Place", b =>
                {
                    b.HasOne("Backend.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");
                });

            modelBuilder.Entity("Backend.Models.Ride", b =>
                {
                    b.HasOne("Backend.Models.Place", "From")
                        .WithMany()
                        .HasForeignKey("FromPlaceId");

                    b.HasOne("Backend.Models.Place", "To")
                        .WithMany()
                        .HasForeignKey("ToPlaceId");
                });
#pragma warning restore 612, 618
        }
    }
}
