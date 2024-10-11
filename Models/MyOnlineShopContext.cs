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
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Product>()
				.Property(p => p.Available)
				.HasDefaultValue(true);

			modelBuilder.Entity<Order>()
				.HasOne(o => o.Customer)
				.WithMany(c => c.Orders)
				.HasForeignKey(o => o.CustomerId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<OrderProduct>()
				.HasOne(od => od.Order)
				.WithMany(o => o.OrderProducts)
				.HasForeignKey(od => od.OrderId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<ImportProduct>()
				.HasOne(ip => ip.Invoice)
				.WithMany(i => i.ImportProducts)
				.HasForeignKey(ip => ip.InvoiceId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ImportProduct>()
				.HasOne(ip => ip.Product)
				.WithMany(p => p.ImportProducts)
				.HasForeignKey(ip => ip.ProductId)
				.OnDelete(DeleteBehavior.Restrict);
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasIndex(u => u.Email)
				.IsUnique();
			modelBuilder.Entity<Category>()
				.HasIndex(c => c.Name)
				.IsUnique();
			modelBuilder.Entity<ProductDetail>()
				.HasIndex(p => p.Name)
				.IsUnique();
		}

	}
}
