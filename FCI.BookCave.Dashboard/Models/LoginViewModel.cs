using System.ComponentModel.DataAnnotations;

namespace FCI.BookCave.Dashboard.Models
{
    public class LoginViewModel
    {
        [StringLength(256)]
        [Display(Name ="User Name")]
        public string UserName { get; set; }

		[DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set; }
    }
}
