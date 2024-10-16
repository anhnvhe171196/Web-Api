﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectWebApi.Data;

#nullable disable

namespace ProjectApi.Migrations
{
    [DbContext(typeof(MyOnlineShopContext))]
    [Migration("20241005043357_updaterole")]
    partial class updaterole
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Available")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool?>("Latest")
                        .HasColumnType("bit");

                    b.Property<bool?>("Special")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProjectApi.Data.ImportProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("ProductId");

                    b.ToTable("ImportProducts");
                });

            modelBuilder.Entity("ProjectApi.Data.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("ReceiptDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("ProjectApi.Data.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Receiver")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("RequireDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ManagerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ManagerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjectWebApi.Data.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float?>("Discount")
                        .HasColumnType("real");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<float?>("UnitPrice")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("ProjectWebApi.Data.ProductDetail", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("IntakePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ProductDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("SellPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("ProjectWebApi.Data.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activated")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("RandomKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("ProjectApi.Models.Manager", b =>
                {
                    b.HasBaseType("ProjectWebApi.Data.User");

                    b.Property<DateTime>("DateOfJoining")
                        .HasColumnType("datetime");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsSeniorManager")
                        .HasColumnType("bit");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Customer", b =>
                {
                    b.HasBaseType("ProjectWebApi.Data.User");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(255)");

                    b.Property<decimal?>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.HasOne("ProjectWebApi.Data.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ProjectApi.Data.ImportProduct", b =>
                {
                    b.HasOne("ProjectApi.Data.Invoice", "Invoice")
                        .WithMany("ImportProducts")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product", "Product")
                        .WithMany("ImportProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjectApi.Data.Invoice", b =>
                {
                    b.HasOne("ProjectApi.Models.Manager", "Manager")
                        .WithMany("Invoices")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("ProjectApi.Data.OrderDetail", b =>
                {
                    b.HasOne("ProjectWebApi.Data.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Order", b =>
                {
                    b.HasOne("ProjectWebApi.Data.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectApi.Models.Manager", null)
                        .WithMany("Orders")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjectWebApi.Data.OrderProduct", b =>
                {
                    b.HasOne("ProjectWebApi.Data.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjectWebApi.Data.ProductDetail", b =>
                {
                    b.HasOne("Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjectApi.Models.Manager", b =>
                {
                    b.HasOne("ProjectWebApi.Data.User", null)
                        .WithOne()
                        .HasForeignKey("ProjectApi.Models.Manager", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectWebApi.Data.Customer", b =>
                {
                    b.HasOne("ProjectWebApi.Data.User", null)
                        .WithOne()
                        .HasForeignKey("ProjectWebApi.Data.Customer", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("ProjectApi.Data.Invoice", b =>
                {
                    b.Navigation("ImportProducts");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("ProjectApi.Models.Manager", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ProjectWebApi.Data.Customer", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
