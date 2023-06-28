using BenchClass.Data;
using BenchClass.Data.Enum;
using BenchClass.Interfaces;
using BenchClass.Models;
using BenchClass.Repository;
using BenchClass.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenchClass.Controllers
{
    public class GymController : Controller
    {
        private readonly IGymRepository _gymRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GymController(IGymRepository gymRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _gymRepository = gymRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }

        //public async Task<IActionResult> Index()
        //{
        //    IEnumerable<Gym> gyms = await _gymRepository.GetAll();
        //    return View(gyms);
        //}

        // start

        public async Task<IActionResult> Index(GymCategory? category)
        {
            IEnumerable<Gym> gyms = await _gymRepository.GetAll();

            if (category.HasValue)
            {
                gyms = gyms.Where(g => g.GymCategory == category);
            }

            return View(gyms);
        }

        //end



        public async Task<IActionResult> Detail(int id)
        {
            Gym gym = await _gymRepository.GetByIdAsync(id);
            return View(gym);
        }
        public async Task<IActionResult> Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var createGymViewModel = new CreateGymViewModel { AppUserId = currentUserId };
            return View(createGymViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGymViewModel gymViewModel)
        {
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(gymViewModel.Image, 700, 1200);
                var gym = new Gym
                {
                    Title = gymViewModel.Title, 
                    Description = gymViewModel.Description,
                    Image = result.Url.ToString(),
                    GymCategory = gymViewModel.GymCategory,
                    AppUserId = gymViewModel.AppUserId,
                    Address = new Address
                    {
                        Street = gymViewModel.Address.Street,
                        City = gymViewModel.Address.City,
                        Country = gymViewModel.Address.Country
                    },
                    GymContact = new GymContact
                    {
                        Email = gymViewModel.GymContact.Email,
                        Phone = gymViewModel.GymContact.Phone
                    }
                };
                _gymRepository.Add(gym);
                return RedirectToAction("Index");

            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed...");
            }

            return View(gymViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {

            var gym = await _gymRepository.GetByIdAsync(id);
            if (gym == null) return View("Error");
            var gymViewModel = new EditGymViewModel
            {
                Title = gym.Title,
                Description = gym.Description,
                AddressId = gym.AddressId,
                Address = gym.Address,
                URL = gym.Image,
                GymCategory = gym.GymCategory,
                GymContactId= gym.GymContactId,
                GymContact = gym.GymContact
            };
            return View(gymViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGymViewModel gymViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit the gym");
                return View("Edit", gymViewModel);
            }

            var userGym = await _gymRepository.GetByIdAsyncNoTracking(id);
            if(userGym != null)
            {   
                if(gymViewModel.Image!= null)
                {
                    try
                    {
                        var fileinfo = new FileInfo(userGym.Image);
                        var publicId = Path.GetFileNameWithoutExtension(fileinfo.Name);
                        await _photoService.DeletePhotoAsync(publicId);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Could not delete the photo");
                    }
                    var photoResult = await _photoService.AddPhotoAsync(gymViewModel.Image, 700, 1200);
                    var gym = new Gym
                    {
                        Id = id,
                        Title = gymViewModel.Title,
                        Description = gymViewModel.Description,
                        Image = photoResult.Url.ToString(),
                        AddressId = gymViewModel.AddressId,
                        Address = gymViewModel.Address,
                        GymCategory = gymViewModel.GymCategory,
                        GymContactId = gymViewModel.GymContactId,
                        GymContact = gymViewModel.GymContact
                    };
                    _gymRepository.Update(gym);
                }

                else
                {
                    var gym = new Gym
                    {
                        Id = id,
                        Title = gymViewModel.Title,
                        Description = gymViewModel.Description,
                        AddressId = gymViewModel.AddressId,
                        Address = gymViewModel.Address,
                        GymCategory = gymViewModel.GymCategory,
                        GymContactId = gymViewModel.GymContactId,
                        GymContact = gymViewModel.GymContact
                    };
                    _gymRepository.Update(gym);


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
            var gymDetails = await _gymRepository.GetByIdAsync(id);
            if (gymDetails == null) return View("Error");
            return View(gymDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteGym(int id)
        {
            var gymDetails = await _gymRepository.GetByIdAsync(id);
            if (gymDetails == null) return View("Error");

            _gymRepository.Delete(gymDetails);
            return RedirectToAction("Index");
        }
    }
}
