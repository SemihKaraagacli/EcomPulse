namespace DataAccessLayer.Entities;

public class Category
{
    public Category()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public ICollection<Product>? Products { get; set; }
}
