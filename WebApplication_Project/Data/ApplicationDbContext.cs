using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebApplication_Project.Areas.Identity.Data;
using WebApplication_Project.Models;

namespace WebApplication_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<CustomUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WebApplication_Project.Models.Product> Product { get; set; }
        public DbSet<WebApplication_Project.Models.Order> Order { get; set; }
        public DbSet<WebApplication_Project.Models.Cart> Cart { get; set; }
        public DbSet<WebApplication_Project.Models.Customer> Customer { get; set; }
        public DbSet<WebApplication_Project.Models.Order_details> Order_Details { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            #region Table products
            var products = modelBuilder.Entity<Product>();
            products.ToTable("Product");
            products.Property(a => a.Name).IsRequired();
            products.Property(a => a.Stock).IsRequired();
            //products.HasOne(a => a.Categorie).WithMany(b => b.Products).HasForeignKey("CategorieID");
            products.HasMany(a => a.Order_Details).WithOne(b => b.Product);
            #endregion

            #region table orders

            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Order>().HasOne(a => a.Customer).WithMany(b => b.Orders).HasForeignKey("Customer_ID");
            modelBuilder.Entity<Order>().Property(p => p.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>().HasMany(a => a.Order_Details).WithOne(b => b.Order);

            #endregion
            #region orderDetails

            var orderDetails = modelBuilder.Entity<Order_details>().ToTable("Order_detail");
            orderDetails.HasKey(p => new { p.Order_ID, p.Product_ID });
            orderDetails.HasOne(a => a.Order).WithMany(b => b.Order_Details).HasForeignKey("Order_ID");
            orderDetails.HasOne(a => a.Product).WithMany(b => b.Order_Details).HasForeignKey("Product_ID");

            #endregion

            #region table Customer

            var customer = modelBuilder.Entity<Customer>().ToTable("Customer");
            customer.Property(a => a.Firstname).IsRequired();
            customer.Property(a => a.Lastname).IsRequired();
            customer.Property(a => a.Email).IsRequired();
            customer.Property(a => a.Password).IsRequired();
            customer.Property(a => a.Phone).IsRequired();
            customer.HasMany(a => a.Orders).WithOne(b => b.Customer);

            #endregion

            #region table Cart

            var cart = modelBuilder.Entity<Cart>().ToTable("Cart");
            cart.Property(a => a.Quantity).IsRequired();
            cart.Property(a => a.Total).IsRequired();
            cart.HasOne(a => a.Customer);


            #endregion
            #region table Categories

            var category = modelBuilder.Entity<Categories>().ToTable("Categorie");
            category.Property(a => a.Name).IsRequired();
            //category.HasMany(a => a.Products);/*.WithOne(b => b.Categorie);*/


            #endregion
        }
    }
}
