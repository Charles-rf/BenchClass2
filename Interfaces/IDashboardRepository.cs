using BenchClass.Models;

namespace BenchClass.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<GymClass>> GetAllUserClasses();
        Task<List<Gym>> GetAllUserGyms();
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);

        bool Update(AppUser user);
        bool Save();

    }
}
