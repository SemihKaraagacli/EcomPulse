using EcomPulse.Repository.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser, AppRole, Guid>(options)
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);
            builder.Entity<OrderItem>().Property(x => x.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Entity<BasketItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");
            builder.Entity<CreditCard>().Property(x => x.AvailableBalance).HasColumnType("decimal(18,2)");
            builder.Entity<Order>().Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
            builder.Entity<OrderItem>().Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Entity<Payment>().Property(x => x.Amount).HasColumnType("decimal(18,2)");
            builder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
