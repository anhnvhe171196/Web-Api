using ProjectApi.Data;
using ProjectApi.DTOs;
using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class CheckProductToImportAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var _context = (MyOnlineShopContext)validationContext.GetService(typeof(MyOnlineShopContext));
			if(value is List<ImportProductDTO> products)
			{
				var invalidProductIds = new List<int>();
                foreach (var item in products)
                {
					var productExits = _context.Products.Any(p => p.Id == item.ProductId);
					if(!productExits)
					{
						invalidProductIds.Add(item.ProductId);
					}
                }
				if(invalidProductIds.Count > 0)
				{
					var invalidIds = string .Join(",", invalidProductIds);
					return new ValidationResult($"Không tồn tại các sản phẩm có id là : {invalidIds}");
				}
            }
			return ValidationResult.Success;
		}
	}
}
