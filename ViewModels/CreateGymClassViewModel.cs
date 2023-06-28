using BenchClass.Data.Enum;
using BenchClass.Models;

namespace BenchClass.ViewModels
{
    public class CreateGymClassViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StrengthCategory? StrengthCategory { get; set; }
        public int EntryFee { get; set; }
        public DateTime? StartTime { get; set; }
        public string? Website { get; set; }
        public string? Contact { get; set; }
        public Address? Address { get; set; }
        public GymCategory? GymCategory { get; set; }
        public GymContact? GymContact { get; set; } 
        public IFormFile Image { get; set; }
        public string AppUserId { get; set; }

    }
}
