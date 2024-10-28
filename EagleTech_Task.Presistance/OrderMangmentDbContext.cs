using EagleTech_Task.Domain.Interfaces;
using EagleTech_Task.Domain.Models;
using EagleTech_Task.Presistance.EntitiesConfiguration;
using EagleTech_Task.Presistance.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace EagleTech_Task.Presistance
{
    public class OrderMangmentDbContext : DbContext
    {
        public OrderMangmentDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker
                .Entries()
                .Where(x => x.Entity is IAuditable && x.State == EntityState.Added)
                .Select(x => x.Entity)
                .Cast<IAuditable>())
            {
                entity.CreationDate = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public virtual DbSet<User>Users { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
