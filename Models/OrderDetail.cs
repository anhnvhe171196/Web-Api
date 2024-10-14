using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Data
{
	public class OrderDetail
	{
		[Key]
		[ForeignKey("Order")]
		public int OrderId { get; set; }

		public DateTime OrderDate { get; set; } = DateTime.Now;
		public DateTime? RequireDate { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string Receiver { get; set; } = null!;

		[Column(TypeName = "nvarchar(255)")]
		public string Address { get; set; } = null!;

		[Column(TypeName = "nvarchar(255)")]
		public string? Description { get; set; }
		public string? Discount { get; set; }
		public decimal Amount { get; set; }
		public string Status { get; set; }
		public virtual Order Order { get; set; } = null!;

	}
}
