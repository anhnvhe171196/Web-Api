using ProjectApi.Models;
using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Data
{
	public class Invoice
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
		public int Id { get; set; }

		[Required]
		public DateTime ReceiptDate { get; set; } 

		[Column(TypeName = "nvarchar(255)")]
		[Required]
		public string Supplier { get; set; } = null!;  

		[Required]
		[Column(TypeName = "decimal(18, 2)")] 
		public decimal TotalAmount { get; set; }
		[ForeignKey("Manager")]
		public Guid ManagerId { get; set; }
		public virtual Manager Manager { get; set; } = null!;
		public virtual ICollection<ImportProduct> ImportProducts { get; set; } = new List<ImportProduct>();
	}
}
