using EagleTech_Task.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace EagleTech_Task.Presistance
{
    public class OrderMangmentDbContext : DbContext
    {
        public OrderMangmentDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public virtual DbSet<User>Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

    }
}
