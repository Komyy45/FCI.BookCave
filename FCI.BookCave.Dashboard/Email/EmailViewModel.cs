using System.ComponentModel.DataAnnotations;

namespace FCI.BookCave.Dashboard.Email
{
    public class EmailViewModel
    {
        [Required]
        [Display(Name ="Email")]
        public string email { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string subject { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string content { get; set; }
    }
}
