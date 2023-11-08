using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet.Models
{
    public class CarMeet
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
        public DateTimeOffset MeetTime { get; set; }
        [Display(Name = "Meet Date")]
        public DateTimeOffset MeetDate { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
    }
}
