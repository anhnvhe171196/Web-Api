using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Helper.Exceptions
{
	public class CheckImageIsValidAttribute : ValidationAttribute
	{
		private readonly string[] _extensions;

		public CheckImageIsValidAttribute(string[] extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
		{
			if (value is IFormFile[] files)
			{
				foreach (var file in files)
				{
					if (file != null)
					{
						var extension = Path.GetExtension(file.FileName).ToLower();
						if (!_extensions.Contains(extension))
						{
							return new ValidationResult($"File '{file.FileName}' không hợp lệ. Vui lòng chỉ có các file có định dạng: {string.Join(", ", _extensions)}");
						}
					}
				}
			}
			return ValidationResult.Success!;
		}
	}
}
