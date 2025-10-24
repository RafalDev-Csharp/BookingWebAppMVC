using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingWebMVC.Controllers
{
    public class HouseNumberController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HouseNumberController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var output = await _dbContext.HouseNumbers.ToListAsync();
            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] HouseNumber houseNumber)
        {
            if (houseNumber == null)
            {
                ModelState.AddModelError(@"model", @"'HouseNumber' object cannot be null");
                return View();
            }
            if (ModelState.IsValid == false)
            {
                ModelState.AddModelError(@"model", @"'HouseNumber' object is not valid, fill the form properly");
                return View(houseNumber);
            }
            try
            {
                await _dbContext.HouseNumbers.AddAsync(houseNumber);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "The house has been created successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception("cannot create new HouseNumber object.. Error occurred.", ex);
            }
            return RedirectToAction(nameof(Index), "HouseNumber");
            //return RedirectToAction(nameof(Index), new { house });
        }


        public async Task<IActionResult> Update(int houseId)
        {
            //var houseToUpdate = _dbContext.Houses.Find(houseId);
            var houseToUpdate = await _dbContext.Houses.FirstOrDefaultAsync(h => h.Id == houseId);
            if (houseToUpdate is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(houseToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] House house)
        {
            var housesList = await _dbContext.Houses.Where(x => string.Equals(x.Name.ToLower(), house.Name.ToLower())).FirstOrDefaultAsync();
            if (housesList != null)
            {
                ModelState.AddModelError("Name", "Model with this name already exists.");
                return View(house);
            }
            if (house == null)
            {
                ModelState.AddModelError("Model", "This house not exists. Probably it was removed.");
                return RedirectToAction("Error", "Home");
            }
            try
            {
                _dbContext.Houses.Update(house);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "The house has been updated successfully.";
                return RedirectToAction("Index", "House");
            }
            catch (Exception ex)
            {
                throw new Exception("cannot create new House object.. Error occurred.", ex);
            }

        }


        public async Task<IActionResult> Delete(int houseId)
        {
            //var houseToUpdate = _dbContext.Houses.Find(houseId);
            var houseToDelete = await _dbContext.Houses.FirstOrDefaultAsync(h => h.Id == houseId);
            if (houseToDelete is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(houseToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(House house)
        {
            var houseToDelete = await _dbContext.Houses.FirstOrDefaultAsync(h => h.Id == house.Id);
            if (houseToDelete is null)
            {
                TempData["error"] = "The house could not be deleted";
                return RedirectToAction("Error", "Home");
            }

            _dbContext.Houses.Remove(houseToDelete);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "The house has been removed successfully.";

            return RedirectToAction("Index", "House");
        }
    }
}
