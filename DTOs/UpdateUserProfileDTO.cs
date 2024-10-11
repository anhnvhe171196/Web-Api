using ProjectApi.Helper.Exceptions.ProjectWebApi.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class UpdateUserProfileDTO
	{
		public string? FullName { get; set; }

		[Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
		[MaxLength(15, ErrorMessage = "Số điện thoại không được quá 15 ký tự.")]
		public string? Phone { get; set; }

		[AllowedImage(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
		public IFormFile? Photo { get; set; }
	}
}
