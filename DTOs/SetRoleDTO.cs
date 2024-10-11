using Newtonsoft.Json.Converters;
using ProjectApi.Helper.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class SetRoleDTO
	{
		[Required]
		[EmailAddress(ErrorMessage = "Email không hợp lệ.")]
		[EmailDoNotExist]
		public string Email { get; set; } = string.Empty;

		[Required]
		[JsonConverter(typeof(StringEnumConverter))]
		public RoleEnum Role { get; set; }

		public bool Active { get; set; }
	}
}
