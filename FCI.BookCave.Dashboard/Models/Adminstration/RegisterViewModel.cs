using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FCI.BookCave.Dashboard.Models.Adminstration
{
    public class RegisterViewModel : IdentityUser
    {
		[StringLength(256)]
		[Display(Name="User Name")]
		public string UserName { get; set; }

		public string phoneNumber { get; set; } = "";


		[StringLength(256)]
		[Display(Name = "Email")]
		[EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
        [DataType(DataType.Password)]
		[Compare("Password")]
        public string ConfirmPassword { get; set; }

	}
}
