using ProjectApi.Helper.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.DTOs
{
	public class CategoryDTO
	{
		[Required(ErrorMessage = "Name is required")]
		[MinLength(4, ErrorMessage = "Name must be at least 4 characters long")]
	
		public string Name { get; set; } = null!;
	}
}
