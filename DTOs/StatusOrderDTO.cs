using ProjectApi.Helpers.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.DTOs
{
	public class StatusOrderDTO
	{
		[FutureDate]
		public DateTime? RequireDate { get; set; }

		[Required]
		public OrderStatus OrderStatus { get; set; }
	}
}
