namespace EcomPulse.Repository.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; } // Primary Key
        public Guid OrderId { get; set; } // Order ile ilişki (Foreign Key)
        public Guid ProductId { get; set; } // Product ile ilişki (Foreign Key)

        public int Quantity { get; set; } // Sipariş edilen ürün miktarı
        public decimal UnitPrice { get; set; } // Sipariş anındaki ürün birim fiyatı
        public decimal TotalPrice => Quantity * UnitPrice; // Ürün toplam fiyatı

        // Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
