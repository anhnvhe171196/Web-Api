using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebApi.Data
{
	[Table("OrderProducts")]
	public class OrderProduct
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("ProductId")]
		public int ProductId { get; set; }
		public virtual Product Product { get; set; } = null!;

		[ForeignKey("OrderId")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; } = null!;

		public float? UnitPrice { get; set; }
		public int Quantity { get; set; }
		public float? Discount { get; set; }
	}

}
