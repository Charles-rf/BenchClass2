using BenchClass.Data;
using BenchClass.Interfaces;
using BenchClass.Models;
using BenchClass.ViewModels;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

namespace BenchClass.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor httpContextAccessor, IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editViewModel, ImageUploadResult photoResult)
        {
            user.Id = editViewModel.Id;
            user.BenchMax = editViewModel.BenchMax;
            user.SquatMax= editViewModel.SquatMax;
            user.DeadliftMax= editViewModel.DeadliftMax;
            user.ProfileImageUrl= photoResult.Url.ToString();
            user.City= editViewModel.City;
            user.Country= editViewModel.Country;
            user.UserDescription= editViewModel.UserDescription;
        }

        public async Task<IActionResult> Index()
        {
            var userClasses = await _dashboardRepository.GetAllUserClasses();
            var userGyms = await _dashboardRepository.GetAllUserGyms();
            var dashboardViewModel = new DashboardViewModel()
            {
                GymClasses = userClasses,
                Gyms = userGyms,
            };
            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currentUserId);
            if (user == null)
            {
                return View("Error");
            }
            var editUserViewModel = new EditUserDashboardViewModel()
            {
                Id = currentUserId,
                BenchMax = user.BenchMax,
                SquatMax = user.SquatMax,
                DeadliftMax = user.DeadliftMax,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                Country = user.Country,
                UserDescription = user.UserDescription,
            };
            return View(editUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editViewModel)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit Profile");
                return View("EditUserProfile", editViewModel);
            }

            var user = await _dashboardRepository.GetByIdNoTracking(editViewModel.Id);


            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editViewModel.Image, 700, 1200);

                MapUserEdit(user, editViewModel, photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }

            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete the photo");
                    return View(editViewModel);
                }

                var photoResult = await _photoService.AddPhotoAsync(editViewModel.Image, 700, 1200);

                MapUserEdit(user, editViewModel, photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }
        }
    }
}
