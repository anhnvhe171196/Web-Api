using Microsoft.EntityFrameworkCore;
using ProjectApi.Data;
using ProjectApi.Models;

namespace ProjectWebApi.Data
{
	public class MyOnlineShopContext : DbContext
	{
		public MyOnlineShopContext(DbContextOptions<MyOnlineShopContext> options) : base(options)
		{
		}

		// Define DbSets for all your entities
		public DbSet<User> Users { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderProduct> OrderProducts { get; set; }
		public DbSet<ProductDetail> ProductDetails { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<Manager> Managers { get; set; }
		public DbSet<ImportProduct> ImportProducts { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
			   .HasOne<User>()
			   .WithOne()
			   .HasForeignKey<Customer>(c => c.Id)
			   .OnDelete(DeleteBehavior.Cascade); // Xóa Customer cũng sẽ xóa User liên quan

			modelBuilder.Entity<Product>()
				.Property(p => p.Available)
				.HasDefaultValue(true); // Giá trị mặc định cho thuộc tính Available

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CustomerId)
				.OnDelete(DeleteBehavior.Cascade); // Xóa Order cũng sẽ xóa các OrderProduct liên quan

			modelBuilder.Entity<OrderProduct>()
				.HasOne(op => op.Order)
				.WithMany(o => o.OrderProducts)
				.HasForeignKey(op => op.OrderId)
				.OnDelete(DeleteBehavior.Cascade); // Xóa OrderProduct khi xóa Order

			modelBuilder.Entity<ImportProduct>()
				.HasOne(ip => ip.Invoice)
				.WithMany(i => i.ImportProducts)
				.HasForeignKey(ip => ip.InvoiceId)
				.OnDelete(DeleteBehavior.Cascade); // Xóa ImportProduct khi xóa Invoice

			modelBuilder.Entity<ImportProduct>()
				.HasOne(ip => ip.Product)
				.WithMany(p => p.ImportProducts)
				.HasForeignKey(ip => ip.ProductId)
				.OnDelete(DeleteBehavior.Restrict); // Không xóa Product khi xóa ImportProduct

			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique(); // Email phải là duy nhất

			modelBuilder.Entity<Category>()
				.HasIndex(c => c.Name)
				.IsUnique(); // Tên Category phải là duy nhất

			modelBuilder.Entity<ProductDetail>()
				.HasIndex(p => p.Name)
				.IsUnique();
		}

	}
}
