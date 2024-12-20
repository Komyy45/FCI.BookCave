using System.ComponentModel.DataAnnotations;

namespace FCI.BookCave.Dashboard.Models.Adminstration
{
    public class ChangePasswordViewModel
    {
        [Display(Name ="Current Password")]
        [DataType(DataType.Password)]
        public string currentPass { get; set; }

        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string newPass { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("newPass")]
        public string confirmPass { get; set; }
    }
}
