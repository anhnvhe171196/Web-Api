using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class Token
	{
		[Required]
		public string AccessToken { get; set; }
		[Required]
		public string RefreshToken { get; set; }
	}
}
