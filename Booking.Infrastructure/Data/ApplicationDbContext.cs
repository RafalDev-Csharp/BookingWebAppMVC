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
        }
    }
}
