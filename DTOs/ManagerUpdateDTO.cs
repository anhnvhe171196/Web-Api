using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class ManagerUpdateDTO
	{
		[Required(ErrorMessage = "Vui lòng chỉ định xem tài khoản có được kích hoạt hay không.")]
		public bool Activated { get; set; }

		[Required(ErrorMessage = "Vui lòng chỉ định xem người dùng có phải là quản lý cao cấp hay không.")]
		public bool IsSeniorManager { get; set; }

		[Required(ErrorMessage = "Mức lương là bắt buộc.")]
		[Range(0, double.MaxValue, ErrorMessage = "Mức lương phải là một số dương.")]
		public decimal Salary { get; set; }
		
	}
}
