using ProjectApi.Helper.Exceptions;
using ProjectApi.Helper.Exceptions.ProjectWebApi.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class ProductDetailDTO 
	{
		[Required]
		[ProductExist]
		public string Name { get; set; } = null!;

		[Required]
		[CheckImageIsValid(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
		public IFormFile[] Image { get; set; } = null!;

		public string? Description { get; set; }

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "Giá đầu vào phải lớn hơn 0")]
		public decimal IntakePrice { get; set; }

		[Required]
		[SellPriceGreaterThanIntakePrice]
		public decimal SellPrice { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
		public int Quantity { get; set; }

		[Required]
		[CategoryExist]
		public string CategoryName { get; set; } = null!;
		public bool? Special { get; set; }
		public bool? Latest { get; set; }
	}
}
