namespace GymBooker.Models
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        TimeSpan Duration { get; set; }
        public DateTime EndTime { get { return StartTime + Duration; } }
        public string Description { get; set; } = string.Empty;
        public ICollection<ApplicationUserGymClass> UserGymClasses { get; set; } = [];
    }
}
