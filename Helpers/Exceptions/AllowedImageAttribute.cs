using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	namespace ProjectWebApi.Helpers.Exceptions
	{
		public class AllowedImageAttribute : ValidationAttribute
		{
			private readonly string[] _extensions;

			public AllowedImageAttribute(string[] extensions)
			{
				_extensions = extensions;
			}

			protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
			{
				if (value is IFormFile file)
				{
					var extension = Path.GetExtension(file.FileName).ToLower();
					if (!_extensions.Contains(extension))
					{
						return new ValidationResult($"File '{file.FileName}' không hợp lệ. Vui lòng chỉ có các file có định dạng: {string.Join(", ", _extensions)}");
					}
				}
				return ValidationResult.Success!;
			}
		}
	}
}