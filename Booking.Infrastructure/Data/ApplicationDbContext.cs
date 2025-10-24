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
            
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<HouseNumber> HouseNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

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
        }
    }
}
