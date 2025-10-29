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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HouseController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var output = _unitOfWork.House.GetAll();
            return View(output);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] House house)
        {
            var housesList = _unitOfWork.House.GetAll().Where(x => string.Equals(x.Name.ToLower(), house.Name.ToLower()));
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
            if (ModelState.IsValid == false)
            {
                ModelState.AddModelError(@"model 'house' is not valid", @"'House' object is not valid, fill the form properly");
                return View(house);
            }
            try
            {
                if (ModelState.IsValid)
                {
                    if (house.Image is not null)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(house.Image.FileName);
                        string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\houseImage");

                        using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                            house.Image.CopyTo(fileStream);

                        house.ImageUrl = @"\images\houseImage\" + fileName;
                    }
                    else
                    {
                        house.ImageUrl = "https://placehold.co/600x400";
                    }
                }
                
                _unitOfWork.House.Add(house);
                await _unitOfWork.SaveAsync();
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
            var houseToUpdate = _unitOfWork.House.Get(x => x.Id == houseId);
            if (houseToUpdate is null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (houseToUpdate.ImageUrl is null)
            {
                houseToUpdate.ImageUrl = "https://placehold.co/600x400";
            }
            return View(houseToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Update(House house)
        {
            if (ModelState.IsValid && house.Id > 0)
            {
                if (house.Image is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(house.Image.FileName);
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\houseImage");
                   
                    if (!string.IsNullOrEmpty(house.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, house.ImageUrl.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                    house.Image.CopyTo(fileStream);

                    house.ImageUrl = @"\images\houseImage\" + fileName;
                }
                else
                {
                    house.ImageUrl = "https://placehold.co/600x400";
                }
            }

            var housesList = _unitOfWork.House.Get((x => string.Equals(x.Name.ToLower(), house.Name.ToLower())));
            if (housesList is not null) { }
            if (house == null)
            {
                ModelState.AddModelError("Model", "This house not exists. Probably it was removed.");
                return RedirectToAction("Error", "Home");
            }
            try
            {
                _unitOfWork.House.Update(house);
                await _unitOfWork.SaveAsync();
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
            var houseToDelete = _unitOfWork.House.Get(h => h.Id == houseId);
            if (houseToDelete is null)
            {
                return RedirectToAction("Error", "Home");
            }
            if (houseToDelete.ImageUrl is null)
            {
                houseToDelete.ImageUrl = "https://placehold.co/600x400";
            }

            return View(houseToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(House house)
        {
            var houseToDelete = _unitOfWork.House.Get(h => h.Id == house.Id);
            if (houseToDelete is null)
            {
                TempData["error"] = "The house could not be deleted";
                return RedirectToAction("Error", "Home");
            }

            if (!string.IsNullOrEmpty(house.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, house.ImageUrl.Trim('\\'));

                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }
            _unitOfWork.House.Remove(houseToDelete);
            await _unitOfWork.SaveAsync();
            TempData["success"] = "The house has been removed successfully.";

            return RedirectToAction("Index", "House");
        }

    }
}
