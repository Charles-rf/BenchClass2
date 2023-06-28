using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BenchClass.Models
{
    public class AppUser : IdentityUser
    {
        public int? BenchMax { get; set; }
        public int? SquatMax { get; set; }
        public int? DeadliftMax { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        [StringLength(50)]
        public string? UserDescription { get; set; }
        public ICollection<Gym>? Gyms { get; set; }
        public ICollection<GymClass>? GymClasses { get; set; }
    }
}
