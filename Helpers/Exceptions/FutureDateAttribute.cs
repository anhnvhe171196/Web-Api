using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helpers.Exceptions
{
	public class FutureDateAttribute : ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if(value is DateTime dateTime)
			{
				if(dateTime < DateTime.Now)
				{
					return new ValidationResult("RequireDate phải lớn hơn ngày hiện tại.");
				}
			}
			return ValidationResult.Success;
		}
	}
}
