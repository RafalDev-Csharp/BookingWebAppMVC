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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]House house)
        {
            if (house == null)
            {
                ModelState.AddModelError(@"model 'house' is null", @"'House' object cannot be null");
                return View();
            }
            if(ModelState.IsValid == false)
            {
                ModelState.AddModelError(@"model 'house' is not valid", @"'House' object is not valid, fill the form properly");
                return View();
            }
            try
            {
                await _dbContext.Houses.AddAsync(house);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("cannot create new House object.. Error occurred.", ex);
            }
            return RedirectToAction(nameof(Index), "House");
        }
    }
}
