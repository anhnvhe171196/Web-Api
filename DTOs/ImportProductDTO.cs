using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.DTOs
{
	public class ImportProductDTO
	{
		[Required]
		public int ProductId { get; set; }
		[Required]
		public int Quantity { get; set; }
		[Required]
		public decimal Price { get; set; }
		
	}
}
