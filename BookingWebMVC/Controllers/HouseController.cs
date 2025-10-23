using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookingWebMVC.Controllers
{
    public class HouseController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var output = _dbContext.Houses.ToList<House>();
            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
