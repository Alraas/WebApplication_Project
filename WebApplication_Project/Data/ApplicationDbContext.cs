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
    }
}
