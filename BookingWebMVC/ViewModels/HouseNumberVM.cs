using Booking.Domain.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookingWebMVC.ViewModels
{
    public class HouseNumberVM
    {
        public HouseNumber? HouseNumber { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? HousesList { get; set; }
    }
}
