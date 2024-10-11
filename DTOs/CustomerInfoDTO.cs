using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class CustomerInfoDTO : UserInfoDTO
	{
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public decimal? Money { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Address { get; set; }
	}
}
