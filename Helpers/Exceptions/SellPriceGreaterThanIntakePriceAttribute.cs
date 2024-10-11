using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class SellPriceGreaterThanIntakePriceAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var model = validationContext.ObjectInstance;

			var intakePriceProperty = model.GetType().GetProperty("IntakePrice");
			if (intakePriceProperty == null)
			{
				return new ValidationResult("Không tìm thấy giá tiền bán ra");
			}
			var intakePriceValue = (decimal?)intakePriceProperty.GetValue(model);
			var sellPriceValue = (decimal?)value;

			if (sellPriceValue.HasValue && intakePriceValue.HasValue && sellPriceValue <= intakePriceValue)
			{
				return new ValidationResult("Giá bán phải lớn hơn giá nhập.");
			}

			return ValidationResult.Success!;
		}
	}
}
