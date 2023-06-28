using BenchClass.Controllers;
using BenchClass.Data;
using BenchClass.Interfaces;
using BenchClass.Models;
using Microsoft.EntityFrameworkCore;

namespace BenchClass.Repository
{
    public class GymClassRepository : IGymClassRepository
    {
        private readonly ApplicationDbContext context;

        public GymClassRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool Add(GymClass gymclass)
        {
            context.Add(gymclass);
            return Save();
        }

        public bool Update(GymClass gymclass)
        {
            context.Update(gymclass);
            return Save();
        }
        public bool Delete(GymClass gymclass)
        {
            context.Remove(gymclass);
            return Save();
        }

        public async Task<IEnumerable<GymClass>> GetAll()
        {
            return await context.GymClasses.ToListAsync();
        }
        public async Task<GymClass> GetByIdAsync(int id)
        {
            return await context.GymClasses.Include(x => x.Address).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<GymClass> GetByIdAsyncNoTracking(int id)
        {
            return await context.GymClasses.Include(x => x.Address).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<IEnumerable<GymClass>> GetAllGymClassesByCity(string city)
        {
            return await context.GymClasses.Where(c => c.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
