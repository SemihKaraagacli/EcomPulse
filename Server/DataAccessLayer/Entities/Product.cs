namespace DataAccessLayer.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
    public ICollection<BasketItem>? BasketItems { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
}
