using BenchClass.Data.Enum;
using BenchClass.Models;

namespace BenchClass.ViewModels
{
    public class EditGymViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? URL { get; set; }
        public IFormFile? Image { get; set; }
        public GymCategory? GymCategory { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public int? GymContactId { get; set; }
        public GymContact? GymContact { get; set; }
    }
}
