using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WebStoreApp.Domain.Models;

namespace WebStoreApp.Infrastructure.DBContext
{
    public class StoreContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        public StoreContext(DbContextOptions<StoreContext> options) : base(options) => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(p => p.Id).IsRequired().UseIdentityColumn();
            modelBuilder.Entity<Cart>().Property(p => p.Id).IsRequired().UseIdentityColumn();
            modelBuilder.Entity<Order>().Property(p => p.Id).IsRequired().UseIdentityColumn();
            modelBuilder.Entity<User>().Property(p => p.Id).IsRequired().UseIdentityColumn();
            modelBuilder.Entity<User>().HasIndex(p => p.Email);
        }
    }
}
