using Meet.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Meet.ViewModels
{
    public class CommentDetail
    {
        public int CommentId { get; set; }
        [Display(Name = "Name")]
        public string CommentorsName { get; set; }
        [Display(Name = "Message")]
        public string CommentBody { get; set; }
        public string Date { get; set; }
        [ForeignKey("CarMeet")]
        public int MeetId { get; set; }
        public bool IsOwner { get; set; }
    }
}
