using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectWebApi.Data
{
	[Table("Users")]
	public class User
	{
		public User()
		{
			Id = Guid.NewGuid();
		}
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string Password { get; set; } = null!;

		[Column(TypeName = "nvarchar(255)")]
		public string FullName { get; set; } = null!;
		[Column(TypeName = "nvarchar(255)")]
		public string RoleName { get; set; }

		[Column(TypeName = "nvarchar(255)")]
		public string Email { get; set; } = null!;

		[Column(TypeName = "nvarchar(15)")]
		public string? Phone { get; set; }
		public bool Activated { get; set; } = true;
		[Column(TypeName = "nvarchar(255)")]
		public string Photo { get; set; } = null!;
		public string? RandomKey { get; set; }
		public bool TwoFactorEnabled { get; set; } = false;
		public string? TwoFactorSecret { get; set; }
		public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
	}

}
