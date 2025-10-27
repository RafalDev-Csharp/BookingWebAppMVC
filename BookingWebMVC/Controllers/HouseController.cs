using Booking.Application.Common.Interfaces;
using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;

namespace BookingWebMVC.Controllers
{
    public class HouseController : Controller
    {
        private readonly IHouseRepository _houseRepository;

        public HouseController(IHouseRepository houseRepository)
        {
            _houseRepository = houseRepository;
        }
        public IActionResult Index()
        {
            var output = _houseRepository.GetAll();
            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm]House house)
        {
            var housesList = _houseRepository.GetAll().Where(x => string.Equals(x.Name.ToLower(), house.Name.ToLower()));
            if (housesList.Count() > 0)
            {
                ModelState.AddModelError("Name", "Model with this name already exists.");
                return View(house);
            }
            if (house == null)
            {
                ModelState.AddModelError(@"model 'house' is null", @"'House' object cannot be null");
                return View();
            }
            if(ModelState.IsValid == false)
            {
                ModelState.AddModelError(@"model 'house' is not valid", @"'House' object is not valid, fill the form properly");
                return View(house);
            }
            try
            {
                _houseRepository.Add(house);
                await _houseRepository.Save();
                TempData["success"] = "The house has been created successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception("cannot create new House object.. Error occurred.", ex);
            }
            return RedirectToAction(nameof(Index), "House");
            //return RedirectToAction(nameof(Index), new { house });
        }

        
        public async Task<IActionResult> Update(int houseId)
        {
            //var houseToUpdate = _dbContext.Houses.Find(houseId);
            var houseToUpdate = _houseRepository.Get(x => x.Id ==  houseId);
            if (houseToUpdate is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(houseToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(House house)
        {
            if (house == null)
            {
                ModelState.AddModelError("Model", "This house not exists. Probably it was removed.");
                return RedirectToAction("Error", "Home");
            }
            try
            {
                _houseRepository.Update(house);
                await _houseRepository.Save();
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
            var houseToDelete = _houseRepository.Get(h => h.Id == houseId);
            if (houseToDelete is null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(houseToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(House house)
        {
            var houseToDelete = _houseRepository.Get(h => h.Id == house.Id);
            if (houseToDelete is null)
            {
                TempData["error"] = "The house could not be deleted";
                return RedirectToAction("Error", "Home");
            }

            _houseRepository.Remove(houseToDelete);
            await _houseRepository.Save();
            TempData["success"] = "The house has been removed successfully.";

            return RedirectToAction("Index", "House");
        }

    }
}
