using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class ChangePasswordDTO
	{
		[Required(ErrorMessage = "Mật khẩu cũ là bắt buộc.")]
		public string OldPassword { get; set; }

		[Required(ErrorMessage = "Mật khẩu mới là bắt buộc.")]
		[MinLength(6, ErrorMessage = "Mật khẩu mới phải có ít nhất 6 ký tự.")]
		[MaxLength(20, ErrorMessage = "Mật khẩu mới không được vượt quá 20 ký tự.")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc.")]
		[Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp với mật khẩu mới.")]
		public string ConfirmPassword { get; set; }
	}
}
