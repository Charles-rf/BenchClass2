using BenchClass.Models;

namespace BenchClass.Interfaces
{
    public interface IGymClassRepository
    {
        Task<IEnumerable<GymClass>> GetAll();
        Task<GymClass> GetByIdAsync(int id);
        Task<GymClass> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<GymClass>> GetAllGymClassesByCity(string city);
        bool Add(GymClass gymclass);
        bool Update(GymClass gymclass);
        bool Delete(GymClass gymclass);
        bool Save();    
    }
}
