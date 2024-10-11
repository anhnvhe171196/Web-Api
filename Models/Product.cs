using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProjectApi.Data;

[Table("Products")]
public class Product
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
	public int Id { get; set; }

	[ForeignKey("CategoryId")]
	public int CategoryId { get; set; }
	public virtual Category Category { get; set; } = null!;

	public bool Available { get; set; } = true;
	public bool? Special { get; set; }
	public bool? Latest { get; set; }
	public virtual ICollection<ImportProduct> ImportProducts { get; set; } = new List<ImportProduct>();
}
