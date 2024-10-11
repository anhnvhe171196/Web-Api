using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Models
{
	public class RegisterCustomerDTO : RegitsterUserDTO
	{
		
		[MaxLength(255, ErrorMessage = "Địa chỉ không được dài quá 255 ký tự.")]
		public string? Address { get; set; }
	}
}
