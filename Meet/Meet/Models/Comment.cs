using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Meet.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        [Display(Name = "Name")]
        public string CommentorsName { get; set; }
        [Display(Name = "Message")]
        public string CommentBody { get; set; }
        public string Date { get; set; }
        [ForeignKey("CarMeet")]
        public int MeetId { get; set; }
        public CarMeet carMeet { get; set; }
    }
}
