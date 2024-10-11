using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWebApi.Data
{
	[Table("Customers")]
	public class Customer : User
	{
		public decimal? Money { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string? Address { get; set; }

	}


}
