using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Meet.Models
{
    public class Attendance
    {
        [Key]
        public int AttendanceId { get; set; }
        public List<Client> Clients { get; set; }
    }
}
