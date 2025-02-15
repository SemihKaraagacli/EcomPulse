namespace DataAccessLayer.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = default!;

    public ICollection<OrderItem>? OrderItems { get; set; }
    public Payment? Payment { get; set; }
}
