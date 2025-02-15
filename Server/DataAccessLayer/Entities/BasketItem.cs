namespace DataAccessLayer.Entities;

public class BasketItem
{
    public Guid Id { get; set; } = default!;
    public Guid BasketId { get; set; } = default!;
    public Basket? Basket { get; set; }
    public Guid ProductId { get; set; } = default!;
    public Product? Product { get; set; }

    public int Quantity { get; set; }
    public decimal Price { get; set; }
}