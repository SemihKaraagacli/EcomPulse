namespace EcomPulse.Repository.Entities
{
    public class Product
    {
        public Guid Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } // Ürün fiyatı
        public int Stock { get; set; } // Stok miktarı

        public ICollection<BasketItem> BasketItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
