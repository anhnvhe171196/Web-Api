using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class CategoryExistAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var category = value.ToString();
			var context = (MyOnlineShopContext)validationContext.GetService(typeof(MyOnlineShopContext));
			if(!context.Categories.Any(c => c.Name == category))
			{
				return new ValidationResult($"Category '{category}' không tồn tại");
			}
			return ValidationResult.Success!;
		}
	}
}
