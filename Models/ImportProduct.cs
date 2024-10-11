using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectApi.Data
{
	public class ImportProduct
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-increment
		public int Id { get; set; }

		[ForeignKey("Invoice")]
		public int InvoiceId { get; set; }

		[ForeignKey("Product")]
		public int ProductId { get; set; } 

		[Required]
		public int Quantity { get; set; } 

		[Required]
		[Column(TypeName = "decimal(18, 2)")] 
		public decimal Price { get; set; }  

		public virtual Invoice Invoice { get; set; } = null!;
		public virtual Product Product { get; set; } = null!;
	}
}
