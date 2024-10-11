using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class ProductExistAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			string nameProduct = value?.ToString();
			var context = (MyOnlineShopContext)validationContext.GetService(typeof(MyOnlineShopContext));
			if (string.IsNullOrEmpty(nameProduct))
			{
				return new ValidationResult("Tên sản phẩm không được để trống.");
			}
			var productId = (int?)validationContext.ObjectType.GetProperty("Id")?.GetValue(validationContext.ObjectInstance);
			var existingProduct = context.ProductDetails
				.Where(p => p.Name == nameProduct && p.ProductId != productId) 
				.Any();

			if (existingProduct)
			{
				return new ValidationResult($"{nameProduct} đã tồn tại.");
			}

			return ValidationResult.Success!;
		}

	}
}
