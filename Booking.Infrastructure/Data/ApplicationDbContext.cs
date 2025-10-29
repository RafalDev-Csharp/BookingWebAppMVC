using Booking.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            //DbContextOptionsBuilder.EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);
        }


        public DbSet<House> Houses { get; set; }
        public DbSet<HouseNumber> HouseNumbers { get; set; }
        public DbSet<Amenity> Amenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<House>().HasData(
                new House
                {
                    Id = 1,
                    Name = "Big House",
                    Description = "Description about Big House...",
                    ImageUrl = "https://placehold.co/600x400",
                    Occupancy = 4,
                    Price = 350,
                    SqMeters = 600
                },
                new House
                {
                    Id = 2,
                    Name = "Uber House",
                    Description = "Description about Uber House...",
                    ImageUrl = "https://placehold.co/600x401",
                    Occupancy = 3,
                    Price = 250,
                    SqMeters = 500
                },
                new House
                {
                    Id = 3,
                    Name = "Boss House",
                    Description = "Description about Boss House...",
                    ImageUrl = "https://placehold.co/600x402",
                    Occupancy = 6,
                    Price = 150,
                    SqMeters = 750
                });

            //HouseNumber
            modelBuilder.Entity<HouseNumber>().HasData(
                new HouseNumber
                {
                    House_Number = 101,
                    HouseId = 1,
                },
                new HouseNumber
                {
                    House_Number = 102,
                    HouseId = 1,
                },
                new HouseNumber
                {
                    House_Number = 103,
                    HouseId = 1,
                },
                new HouseNumber
                {
                    House_Number = 104,
                    HouseId = 1,
                },
                new HouseNumber
                {
                    House_Number = 201,
                    HouseId = 2,
                },
                new HouseNumber
                {
                    House_Number = 202,
                    HouseId = 2,
                },
                new HouseNumber
                {
                    House_Number = 203,
                    HouseId = 2,
                },
                new HouseNumber
                {
                    House_Number = 301,
                    HouseId = 3,
                },
                new HouseNumber
                {
                    House_Number = 302,
                    HouseId = 3,
                }
                );

            modelBuilder.Entity<Amenity>().HasData(
         new Amenity
         {
             Id = 1,
             HouseId = 1,
             Name = "Private Pool"
         }, new Amenity
         {
             Id = 2,
             HouseId = 1,
             Name = "Microwave"
         }, new Amenity
         {
             Id = 3,
             HouseId = 1,
             Name = "Private Balcony"
         }, new Amenity
         {
             Id = 4,
             HouseId = 1,
             Name = "1 king bed and 1 sofa bed"
         },

         new Amenity
         {
             Id = 5,
             HouseId = 2,
             Name = "Private Plunge Pool"
         }, new Amenity
         {
             Id = 6,
             HouseId = 2,
             Name = "Microwave and Mini Refrigerator"
         }, new Amenity
         {
             Id = 7,
             HouseId = 2,
             Name = "Private Balcony"
         }, new Amenity
         {
             Id = 8,
             HouseId = 2,
             Name = "king bed or 2 double beds"
         },

         new Amenity
         {
             Id = 9,
             HouseId = 3,
             Name = "Private Pool"
         }, new Amenity
         {
             Id = 10,
             HouseId = 3,
             Name = "Jacuzzi"
         }, new Amenity
         {
             Id = 11,
             HouseId = 3,
             Name = "Private Balcony"
         });
        }
    }
}
