using Booking.Domain.Entities;
using Booking.Infrastructure.Data;
using BookingWebMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var output = await _dbContext.HouseNumbers.Include(x => x.House).ToListAsync();
            return View(output);
        }

        public async Task<IActionResult> Create()
        {
            HouseNumberVM houseNumberVM = new()
            {
                HousesList = _dbContext.Houses.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                //HouseNumber = await _dbContext.HouseNumbers
            };
            // ViewData used to provide data from controller to the view
            //is a type of Dictionary
            //ViewBag is a dynamic type property
            //ViewData["HousesSelectListItems"] = list;
            //ViewBag.MyHousesList = list;
            return View(houseNumberVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(HouseNumberVM houseNumberVM)
        {
            bool isHouseNumberExists = _dbContext.HouseNumbers.Any(x => x.House_Number == houseNumberVM.HouseNumber.House_Number);
            if (isHouseNumberExists)
            {
                TempData["error"] = "The House Number already exists, enter the another number";
                houseNumberVM = new()
                {
                    HousesList = _dbContext.Houses.ToList().Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),
                };
                return View(houseNumberVM);
            }
            if (houseNumberVM == null)
            {
                ModelState.AddModelError(@"model", @"'HouseNumber' object cannot be null");
                TempData["error"] = "The House Number cannot be null";
                return View(houseNumberVM);
            }
            if (ModelState.IsValid == false)
            {
                ModelState.AddModelError(@"model", @"'HouseNumber' object is not valid, fill the form properly");
                TempData["error"] = "The House Number is not valid";
                return View(houseNumberVM);
            }
            try
            {
                await _dbContext.HouseNumbers.AddAsync(houseNumberVM.HouseNumber);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "The house has been created successfully.";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Something went wrong while saving House Number to database";
                throw new Exception("cannot create new HouseNumber object.. Error occurred.", ex);
            }
            return RedirectToAction(nameof(Index), "HouseNumber");
        }



        public async Task<IActionResult> Update(int houseNumberId)
        {
            HouseNumberVM houseNumberToUpdateVM = new();
            houseNumberToUpdateVM.HousesList = _dbContext.Houses.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            houseNumberToUpdateVM.HouseNumber = await _dbContext.HouseNumbers.Include(x => x.House).FirstOrDefaultAsync(h => h.House_Number == houseNumberId);
            if (houseNumberToUpdateVM.HouseNumber is null)
            {
                TempData["error"] = $"Something went wrong while reading the data of House Number with Id = {houseNumberId}";
                return RedirectToAction("Error", "Home");
            }
            return View(houseNumberToUpdateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HouseNumberVM? houseNumberToUpdateVM)
        {
            //var houseNumberExists = await _dbContext.HouseNumbers.Where(x => x.House_Number == houseNumberToUpdateVM.HouseNumber.House_Number).ToListAsync();
            houseNumberToUpdateVM.HousesList = _dbContext.Houses.ToList()
                .Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            //if (houseNumberExists.Count > 1)
            //{
            //    ModelState.AddModelError("Name", "Model with this number/Id already exists.");
            //    TempData["error"] = "House Number with Id = { } already exists";
            //    return View(houseNumberToUpdateVM);
            //}
            if (houseNumberToUpdateVM.HouseNumber == null)
            {
                ModelState.AddModelError("Model", "This house not exists. Probably it was removed.");
                TempData["error"] = "This house not exists. Probably it was removed.";
                return RedirectToAction("Error", "Home");
            }
            try
            {
                _dbContext.HouseNumbers.Update(houseNumberToUpdateVM.HouseNumber);
                await _dbContext.SaveChangesAsync();
                TempData["success"] = "The House Number has been updated successfully.";
                return RedirectToAction("Index", "HouseNumber");
            }
            catch (Exception ex)
            {
                throw new Exception("cannot create new House Number object.. some Error occurred.", ex);
            }

        }


        public async Task<IActionResult> Delete(int houseNumberId)
        {
            HouseNumberVM houseNumberToDeleteVM = new();
            houseNumberToDeleteVM.HousesList = _dbContext.Houses.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            houseNumberToDeleteVM.HouseNumber = await _dbContext.HouseNumbers.Include(x => x.House).FirstOrDefaultAsync(h => h.House_Number == houseNumberId);
            if (houseNumberToDeleteVM.HouseNumber is null)
            {
                TempData["error"] = $"Something went wrong while reading the data of House Number with Id = {houseNumberId}";
                return RedirectToAction("Error", "Home");
            }
            return View(houseNumberToDeleteVM);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(HouseNumberVM? houseNumberToDeleteVM)
        {
            houseNumberToDeleteVM.HousesList = _dbContext.Houses.ToList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                });
            houseNumberToDeleteVM.HouseNumber = await _dbContext.HouseNumbers.FirstOrDefaultAsync(x => x.House_Number == houseNumberToDeleteVM.HouseNumber.House_Number);
            if (houseNumberToDeleteVM.HouseNumber is null)
            {
                TempData["error"] = "The house number could not be deleted";
                return RedirectToAction("Error", "Home");
            }

            _dbContext.HouseNumbers.Remove(houseNumberToDeleteVM.HouseNumber);
            await _dbContext.SaveChangesAsync();
            TempData["success"] = "The house has been removed successfully.";

            return RedirectToAction("Index", "HouseNumber");
        }
    }
}
