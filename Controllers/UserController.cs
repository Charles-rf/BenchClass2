using BenchClass.Interfaces;
using BenchClass.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BenchClass.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    BenchMax = user.BenchMax,
                    SquatMax = user.SquatMax,
                    DeadliftMax = user.DeadliftMax,
                    ProfileImageUrl = user.ProfileImageUrl,
                    UserDescription = user.UserDescription
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                BenchMax = user.BenchMax,
                SquatMax = user.SquatMax,
                DeadliftMax = user.DeadliftMax,
                ProfileImageUrl = user.ProfileImageUrl,
                City= user.City,
                Country= user.Country,
                UserDescription= user.UserDescription,
            };
            return View(userDetailViewModel);
        }
    }
}
