namespace BenchClass.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public int? BenchMax { get; set; }
        public int? SquatMax { get; set; }
        public int? DeadliftMax { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? UserDescription { get; set; }
    }
}
