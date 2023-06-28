using BenchClass.Data;
using BenchClass.Interfaces;
using BenchClass.Models;
using Microsoft.EntityFrameworkCore;

namespace BenchClass.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<GymClass>> GetAllUserClasses()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userClasses = _context.GymClasses.Where(r => r.AppUser.Id == currentUser);
            return userClasses.ToList();
        }

        public async Task<List<Gym>> GetAllUserGyms()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userGyms = _context.Gyms.Where(r => r.AppUser.Id == currentUser);
            return userGyms.ToList();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser> GetByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
