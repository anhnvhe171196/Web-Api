using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectWebApi.Data
{
	[Table("Category")]
	public class Category
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Tự động tăng
		public int Id { get; set; }
		[Column(TypeName = "nvarchar(100)")]
		public string Name { get; set; } = null!;

		public virtual ICollection<Product> Products { get; set; } = new List<Product>();

	}
}
