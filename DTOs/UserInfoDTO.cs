using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class UserInfoDTO
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string RoleName { get; set; }
		public string Phone { get; set; }
		public bool Activated { get; set; }
		public string Photo { get; set; }

		
	}
}
