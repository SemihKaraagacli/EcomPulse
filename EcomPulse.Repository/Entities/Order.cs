namespace EcomPulse.Repository.Entities
{
    public class Order
    {
        public Guid Id { get; set; } // Primary Key
        public Guid UserId { get; set; } // Kullanıcı kimliği (Foreign Key)
        public AppUser User { get; set; }
        public DateTime CreatedAt { get; set; } // Sipariş tarihi
        public decimal TotalAmount { get; set; } // Sipariş toplam tutarı
        public string OrderStatus { get; set; } // Örneğin: Pending, Completed

        public ICollection<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
    }
}
