using BenchClass.Data;
using BenchClass.Interfaces;
using BenchClass.Models;
using Microsoft.EntityFrameworkCore;

namespace BenchClass.Repository
{
    public class GymRepository : IGymRepository
    {
        private readonly ApplicationDbContext context;
        public GymRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(Gym gym)
        {
            context.Add(gym);
            return Save();
        }

        public bool Update(Gym gym)
        {
            context.Update(gym);
            return Save();
        }
        public bool Delete(Gym gym)
        {
            context.Remove(gym);
            return Save();
        }

        public async Task<IEnumerable<Gym>> GetAll()
        {
            return await context.Gyms.ToListAsync();
        }
        public async Task<Gym> GetByIdAsync (int id)
        {
            return await context.Gyms.Include(x => x.Address).Include(y => y.GymContact).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<Gym> GetByIdAsyncNoTracking(int id)
        {
            return await context.Gyms.Include(x => x.Address).Include(y => y.GymContact).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<Gym>> GetGymByCity(string city)
        {
            return await context.Gyms.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
