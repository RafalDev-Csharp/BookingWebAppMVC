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
    public class HouseNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "House Number")]
        public int House_Number { get; set; }

        [ForeignKey("House")]
        [Display(Name = "House Id")]
        public int HouseId { get; set; }

        [ValidateNever]
        public House House { get; set; }

        [Display(Name = "Special Details")]
        public string? SpecialDetails { get; set; }
    }
}
