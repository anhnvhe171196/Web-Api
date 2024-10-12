using ProjectApi.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	[CheckProductAndQuantity]
	public class AddProductToCartDTO
	{
		[Required]
		public string ProductName { get; set; }
		[Required]
		public int Quantity { get; set; }
	}
}
