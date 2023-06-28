using System.ComponentModel.DataAnnotations;

namespace BenchClass.Models
{
    public class GymContact
    {
        [Key]
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }

    }
}
