using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class House
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Display(Name="Price per day")]
        public double Price { get; set; }
        public int SqMeters { get; set; } //square meters
        public int Occupancy { get; set; }

        [Display(Name ="Image Url")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
