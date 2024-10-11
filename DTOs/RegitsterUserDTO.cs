using ProjectApi.Helper.Exceptions;
using ProjectApi.Helper.Exceptions.ProjectWebApi.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
	public class RegitsterUserDTO
	{
		[Required]
		[EmailAddress(ErrorMessage = "Email không hợp lệ.")]
		[UniqueEmail]
		public string Email { get; set; } = string.Empty;

		[Required]
		[MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string FullName { get; set; } = string.Empty;
		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
		[MaxLength(15, ErrorMessage = "Số điện thoại không được quá 15 ký tự.")]
		public string? Phone { get; set; }

	}
}
