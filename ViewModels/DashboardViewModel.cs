using BenchClass.Models;

namespace BenchClass.ViewModels
{
    public class DashboardViewModel
    {
        public List<GymClass> GymClasses { get; set; }
        public List<Gym> Gyms { get; set; }
        public string UserName { get; set; }
    }
}
