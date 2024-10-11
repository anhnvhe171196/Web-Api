using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class CheckDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value is DateTime receiptDate)
			{
				if (receiptDate > DateTime.Now)
				{
					return new ValidationResult("Thời gian vào kho phải nhỏ hơn thời gian hiện tại.");
				}
			}

			return ValidationResult.Success;
		}
	}
}
