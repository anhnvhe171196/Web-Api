using ProjectApi.DTOs;
using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helpers.Exceptions
{
	public class CheckProductAndQuantityAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var model = value as AddProductToCartDTO;
			var _context = validationContext.GetService(typeof(MyOnlineShopContext)) as MyOnlineShopContext;
			var productIsExist = _context.ProductDetails.SingleOrDefault(p => p.Name == model.ProductName);
			if (productIsExist == null)
			{
				return new ValidationResult($"Không tồn tài sản phẩm {model.ProductName}");
			}
			if(model.Quantity < 0)
			{
				return new ValidationResult("Vui lòng nhập số lượng lớn hơn 0");
			}
			if(model.Quantity > productIsExist.Quantity)
			{
				return new ValidationResult($"Vui lòng nhập số lượng nhở hơn {productIsExist.Quantity}");
			}
			return ValidationResult.Success;
		}
	}
}
