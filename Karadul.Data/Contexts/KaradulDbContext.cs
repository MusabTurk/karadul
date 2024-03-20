using Karadul.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Karadul.Data.DbContexts
{
    public class KaradulDbContext : DbContext
    {
        public KaradulDbContext(DbContextOptions<KaradulDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CategoryProduct>()
        //.HasKey(cp => new { cp.CategoryId, cp.ProductId });

        //    modelBuilder.Entity<CategoryProduct>()
        //        .HasOne(cp => cp.Category)
        //        .WithMany(c => c.CategoryProducts)
        //        .HasForeignKey(cp => cp.CategoryId);

        //    modelBuilder.Entity<CategoryProduct>()
        //        .HasOne(cp => cp.Product)
        //        .WithMany(p => p.CategoryProducts)
        //        .HasForeignKey(cp => cp.ProductId);
        //}
    }
}
