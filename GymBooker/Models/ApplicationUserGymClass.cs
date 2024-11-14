namespace GymBooker.Models
{
    public class ApplicationUserGymClass
    {
        public string ApplicationUserId { get; set; } = string.Empty;
        public int GymClassId { get; set; }


        public ApplicationUser ApplicationUser { get; set; } = null!;
        public GymClass GymClass { get; set; } = null!;
    }
}
