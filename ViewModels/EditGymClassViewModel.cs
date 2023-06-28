using BenchClass.Data.Enum;
using BenchClass.Models;

namespace BenchClass.ViewModels
{
    public class EditGymClassViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? URL { get; set; }
        public IFormFile? Image { get; set; }
        public string? Website { get; set; }
        public string? Contact { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public GymContact? GymContact { get; set; }
        public int? EntryFee { get; set; }
        public StrengthCategory? StrengthCategory { get; set; }
        public GymCategory? GymCategory { get; set; }
        public DateTime? StartTime { get; set; }

    }
}
