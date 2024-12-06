namespace EcomPulse.Repository.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; } // Primary Key
        public Guid BasketId { get; set; } // Basket ile ilişki (Foreign Key)
        public Basket Basket { get; set; }// Navigation Properties
        public Guid ProductId { get; set; } // Product ile ilişki (Foreign Key)
        public Product Product { get; set; }// Navigation Properties

        public int Quantity { get; set; } // Sepetteki ürün miktarı
        public decimal Price { get; set; } // Ürün fiyatı (sepete eklenme anındaki fiyat)
    }
}