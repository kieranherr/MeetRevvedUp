using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet.Models
{
    public class Meet
    {
        [Key]
        public int MeetId { get; set; }
        public string MeetName { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long Zip { get; set; }
        public string MeetTime { get; set; }
        public string MeetDate { get; set; }
        [ForeignKey("CarId")]
        public Car Car { get; set; } 
    }
}
