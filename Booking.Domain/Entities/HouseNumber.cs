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
        public int House_Number { get; set; }

        [ForeignKey("House")]
        public int HouseId { get; set; }
        public House House { get; set; }
        public string? SpecialDetails { get; set; }
    }
}
