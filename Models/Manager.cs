using ProjectApi.Data;
using ProjectWebApi.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectApi.Models
{
	[Table("Managers")]
	public class Manager : User
	{
		[Required]
		[Column(TypeName = "nvarchar(50)")]
		public string EmployeeCode { get; set; } = null!;

		[Required]
		[Column(TypeName = "datetime")]
		public DateTime DateOfJoining { get; set; } = DateTime.Now;
		public bool IsSeniorManager { get; set; } = false;

		[Column(TypeName = "decimal(18,2)")]
		public decimal? Salary { get; set; }

		public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
	}
}
