namespace EcomPulse.Repository.Entities
{
    public class CreditCard
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; } // Kullanıcı ile ilişki
        public AppUser User { get; set; }

        public string CardHolderName { get; set; } // Kart üzerindeki isim
        public string CardNumber { get; set; } // Sadece son 4 hanesi tutulur
        public string ExpirationDate { get; set; } // Geçerlilik tarihi (MM/YY)
        public string CVV { get; set; } // Şifrelenmiş ya da sadece doğrulama için kullanılır
        public DateTime AddedDate { get; set; } // Kartın sisteme eklendiği tarih

        public ICollection<Payment> Payments { get; set; }
    }
}
