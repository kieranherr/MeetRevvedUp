using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet.Models
{
    public class ClientMeet
    {
        [Key]
        public int ClientMeetId { get; set; }
        [ForeignKey("CarMeet")]
        public int MeetId { get; set; }
        public CarMeet carMeet { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client client { get; set; }
    }
}
