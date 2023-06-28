using BenchClass.Data;
using BenchClass.Data.Enum;
using BenchClass.Interfaces;
using BenchClass.Models;
using BenchClass.Repository;
using BenchClass.Services;
using BenchClass.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenchClass.Controllers
{
    public class GymClassController : Controller
    {
        private readonly IGymClassRepository gymClassRepository;
        private readonly IPhotoService photoService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GymClassController(IGymClassRepository gymClassRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            this.gymClassRepository = gymClassRepository;
            this.photoService = photoService;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<GymClass> gymClasses = await gymClassRepository.GetAll();
            return View(gymClasses);
        }
        public async Task<IActionResult> Detail(int id)
        {
            GymClass gymClasses = await gymClassRepository.GetByIdAsync(id);
            return View(gymClasses);

        }
        public async Task<IActionResult> Create()
        {
            var currentUserId = httpContextAccessor.HttpContext?.User.GetUserId();
            var createGymClassViewModel = new CreateGymClassViewModel { AppUserId = currentUserId };
            return View(createGymClassViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateGymClassViewModel gymClassViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await photoService.AddPhotoAsync(gymClassViewModel.Image, 700, 1200);
                var gymClass = new GymClass
                {
                    Title = gymClassViewModel.Title,
                    Description = gymClassViewModel.Description,
                    Image = result.Url.ToString(),
                    AppUserId = gymClassViewModel.AppUserId,
                    StartTime = gymClassViewModel.StartTime,
                    EntryFee = gymClassViewModel.EntryFee,
                    Website = gymClassViewModel.Website,
                    Contact = gymClassViewModel.Contact,
                    StrengthCategory = gymClassViewModel.StrengthCategory,
                    Address = new Address
                    {
                        Street = gymClassViewModel.Address.Street,
                        City = gymClassViewModel.Address.City,
                        Country= gymClassViewModel.Address.Country,
                    }
                };
                gymClassRepository.Add(gymClass);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed...");
            }   

            return View(gymClassViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var gymClass = await gymClassRepository.GetByIdAsync(id);
            if (gymClass == null) return View("Error");
            var gymViewModel = new EditGymClassViewModel
            {
                Title = gymClass.Title,
                Description = gymClass.Description,
                StrengthCategory = gymClass.StrengthCategory,
                EntryFee = gymClass.EntryFee,
                AddressId = gymClass.AddressId,
                Address = gymClass.Address,
                URL = gymClass.Image,
                Website = gymClass.Website,
                Contact = gymClass.Contact,
            };
            return View(gymViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGymClassViewModel gymViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit the gym");
                return View("Edit", gymViewModel);
            }

            var userGym = await gymClassRepository.GetByIdAsyncNoTracking(id);
            if (userGym != null)
            {
                if (gymViewModel.Image != null)
                {
                    try
                    {
                        var fileinfo = new FileInfo(userGym.Image);
                        var publicId = Path.GetFileNameWithoutExtension(fileinfo.Name);
                        await photoService.DeletePhotoAsync(publicId);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete the photo");
                    }
                    var photoResult = await photoService.AddPhotoAsync(gymViewModel.Image, 700, 1200);
                    var gymClass = new GymClass
                    {
                        Id = id,
                        Title = gymViewModel.Title,
                        Description = gymViewModel.Description,
                        Image = photoResult.Url.ToString(),
                        AddressId = gymViewModel.AddressId,
                        Address = gymViewModel.Address,
                        StrengthCategory = gymViewModel.StrengthCategory,
                        EntryFee= gymViewModel.EntryFee,
                        Website= gymViewModel.Website,
                        Contact= gymViewModel.Contact
                        
                    };
                    gymClassRepository.Update(gymClass);

                }

                else
                {
                    var gymClass = new GymClass
                    {
                        Id = id,
                        Title = gymViewModel.Title,
                        Description = gymViewModel.Description,
                        AddressId = gymViewModel.AddressId,
                        Address = gymViewModel.Address,
                        StrengthCategory = gymViewModel.StrengthCategory,
                        EntryFee = gymViewModel.EntryFee,
                        Website = gymViewModel.Website,
                        Contact = gymViewModel.Contact
                    };
                    gymClassRepository.Update(gymClass);


                }
                return RedirectToAction("Index");

            }
            else
            {
                return View(gymViewModel);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var gymClassDetails = await gymClassRepository.GetByIdAsync(id);
            if (gymClassDetails == null) return View("Error");
            return View(gymClassDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            var gymClassDetails = await gymClassRepository.GetByIdAsync(id);
            if (gymClassDetails == null) return View("Error");

            gymClassRepository.Delete(gymClassDetails);
            return RedirectToAction("Index");
        }
    }

}