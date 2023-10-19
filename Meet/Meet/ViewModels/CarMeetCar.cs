using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Meet.ViewModels
{
    public class CarMeetCar
    {
        [Key]
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public string Mods { get; set; }
        [Display(Name = "Image Location")]
        public string ImageLocation { get; set; }
        [Display(Name = "Rating")]
        public int AvgRating { get; set; }
        public int MeetId { get; set; }
        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
