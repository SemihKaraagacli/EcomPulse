using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer;

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
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Order>()
            .HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId);
        builder.Entity<OrderItem>().Property(x => x.TotalPrice).HasColumnType("money");
        builder.Entity<BasketItem>().Property(x => x.Price).HasColumnType("money");
        builder.Entity<CreditCard>().Property(x => x.AvailableBalance).HasColumnType("money");
        builder.Entity<Order>().Property(x => x.TotalAmount).HasColumnType("money");
        builder.Entity<OrderItem>().Property(x => x.UnitPrice).HasColumnType("money");
        builder.Entity<Payment>().Property(x => x.Amount).HasColumnType("money");
        builder.Entity<Product>().Property(x => x.Price).HasColumnType("money");
    }
}
