using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProjectApi.Data;

namespace ProjectWebApi.Data
{
	[Table("Orders")]
	public class Order
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[ForeignKey("Customer")]
		public Guid CustomerId { get; set; }
		public virtual Customer Customer { get; set; } = null!;

		public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
	}

}
