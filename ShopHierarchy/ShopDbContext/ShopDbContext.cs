using Microsoft.EntityFrameworkCore;
using ShopHierarchy.Data;

namespace ShopHierarchy.ShopDbContext
{
    public class ShopDbContext : DbContext
    {
        public DbSet<SalesMan> Salesmans { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=.;Database=MyTestShop;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Customer>()
                .HasOne(c => c.Salesman)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SalesmanId);

            builder
                .Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            builder
                .Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);

            // Композитен ключ
            builder
                .Entity<ItemOrder>()
                .HasKey(k => new { k.ItemId, k.OrderId });

            builder
                .Entity<Item>()
                .HasMany(i => i.Orders)
                .WithOne(k => k.Item)
                .HasForeignKey(k => k.ItemId);

            builder
                .Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(k => k.Order)
                .HasForeignKey(k => k.OrderId);

            builder
                .Entity<Review>()
                .HasOne(r => r.Item)
                .WithMany(i => i.Reviews)
                .HasForeignKey(r => r.ItemId);
        }
    }
}
