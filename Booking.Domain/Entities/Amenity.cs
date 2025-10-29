using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entities
{
    public class Amenity
    {
        [Key]
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        [ValidateNever]
        public House House { get; set; }

        [ForeignKey("House")]
        public int HouseId { get; set; }
    }
}
