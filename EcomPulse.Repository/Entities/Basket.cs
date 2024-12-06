namespace EcomPulse.Repository.Entities
{
    public class Basket
    {
        public Guid Id { get; set; } // Primary Key
        public Guid UserId { get; set; } // Kullanıcı kimliği (Foreign Key)
        public AppUser User { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
        public decimal TotalPrice => BasketItems.Sum(item => item.Quantity * item.Price); // Dinamik toplam fiyat
    }
}
