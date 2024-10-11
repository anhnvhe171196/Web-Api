using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class UniqueEmailAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var dbContext = (MyOnlineShopContext)validationContext.GetService(typeof(MyOnlineShopContext));
			var email = value as string;

			if (dbContext.Users.Any(u => u.Email == email))
			{
				return new ValidationResult("Email đã tồn tại.");
			}

			return ValidationResult.Success;
		}
	}
}
