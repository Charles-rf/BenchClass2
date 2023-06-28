using BenchClass.Models;

namespace BenchClass.Interfaces
{
    public interface IGymRepository
    {
        Task<IEnumerable<Gym>> GetAll();
        Task<Gym> GetByIdAsync(int id);
        Task<Gym> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Gym>> GetGymByCity(string city);
        bool Add(Gym gym);
        bool Update(Gym gym);
        bool Delete(Gym gym);
        bool Save();
    }
}
