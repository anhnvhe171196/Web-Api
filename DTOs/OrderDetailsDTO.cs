using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProjectApi.DTOs
{
	public class OrderDetailsDTO
	{
		public string Receiver { get; set; } = null!;
		public string Address { get; set; } = null!;
		public DateTime OrderDate { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public DateTime? RequireDate { get; set; }

		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Description { get; set; }
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		public string? Discount { get; set; }
		public List<OrderProductDTO> OrderProducts { get; set; }
	}
}
