using ProjectApi.Helper.Exceptions;
using ProjectApi.Helper.Exceptions.ProjectWebApi.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class UpdateProductDTO
	{
		[Required]
		[ProductExist(ErrorMessage = "Sản phẩm không tồn tại.")]
		public string Name { get; set; }

		[CheckImageIsValid(new string[] { ".jpg", ".jpeg", ".png", ".gif" })]
		public IFormFile[]? Image { get; set; }
		public string? Description { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Giá đầu vào phải lớn hơn 0.")]
		public decimal? IntakePrice { get; set; }

		[SellPriceGreaterThanIntakePrice]
		public decimal? SellPrice { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0.")]
		public int? Quantity { get; set; }
		[Required]
		[CategoryExist(ErrorMessage = "Danh mục không tồn tại.")]
		public string CategoryName { get; set; }
		public bool? Special { get; set; }
		public bool? Latest { get; set; }
	}
}
