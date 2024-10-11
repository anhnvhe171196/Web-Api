using ProjectApi.Helper.Exceptions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class UserLogin
	{
		[Required]
		[EmailAddress(ErrorMessage = "Email không hợp lệ.")]
		public string Email { get; set; }
		[Required]
		public string Password { get; set; }

	}
}
