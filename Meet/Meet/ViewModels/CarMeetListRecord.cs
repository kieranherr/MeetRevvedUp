using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Meet.ViewModels
{
    public class CarMeetListRecord
    {
        [Key]
        public int MeetId { get; set; }
        [Display(Name = "Meet Name")]
        public string MeetName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long Zip { get; set; }
        [Display(Name = "Meet Time")]
        public string MeetTime { get; set; }
        [Display(Name = "Meet Date")]
        public string MeetDate { get; set; }
        public double UserLat { get; set; }
        public double UserLong { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
