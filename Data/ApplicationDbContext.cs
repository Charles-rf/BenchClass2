using BenchClass.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BenchClass.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<GymContact> GymContacts { get; set; }
    }
}
