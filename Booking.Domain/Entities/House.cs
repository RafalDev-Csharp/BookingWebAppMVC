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

        [MaxLength(50)]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Display(Name = "Price per day")]
        [Range(10, 10000)]
        public double Price { get; set; }
        public int SqMeters { get; set; } //square meters

        [Range(1, 12)]
        public int Occupancy { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
