using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.DTOs
{
	public class OrderDTO
	{
		[Required]
		public string Receiver { get; set; } = null!;
		[Required]
		public string Address { get; set; } = null!;
		public string? Description { get; set; }
		public string? Discount { get; set; }
		[Required]
		public bool Transfer {  get; set; }
	}
}
