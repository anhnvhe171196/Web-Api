using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebApi.Data
{
	public class ProductDetail
	{
		[Key]
		[ForeignKey("Product")]
		public int ProductId { get; set; } 

		[Required]
		[StringLength(100)]
		public string Name { get; set; } = null!;
		[Required]
		public string Image { get; set; } = null!;
		[Required]
		public DateTime ProductDate { get; set; } = DateTime.Now;

		public string? Description { get; set; } = null!; 

		[Required]
		[Range(0.01, double.MaxValue, ErrorMessage = "IntakePrice phải lớn hơn 0")]
		public decimal IntakePrice { get; set; }
		[Required]
		public decimal SellPrice { get; set; }

		[Required]
		[Range(1, int.MaxValue, ErrorMessage = "Quantity phải lớn hơn 0")]
		public int Quantity { get; set; }

		public virtual Product Product { get; set; } = null!;
	}

}
