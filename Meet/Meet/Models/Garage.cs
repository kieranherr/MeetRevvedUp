using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet.Models
{
    public class Garage
    {
        [Key]
        public int GarageId { get; set; }
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }

        [ForeignKey("IdentityUser")]
        public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

    }
}
