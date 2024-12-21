﻿namespace EcomPulse.Repository.Entities
{
    public class CreditCard
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public string CardHolderName { get; set; } // Kart üzerindeki isim
        public string CardNumber { get; set; } // Sadece son 4 hanesi tutulur
        public DateTime ExpirationDate { get; set; }
        public string ExpirationDateFormatted => ExpirationDate.ToString("MM/yyyy"); // Geçerlilik tarihi (MM/YYYY)
        public string CVV { get; set; } // Şifrelenmiş ya da sadece doğrulama için kullanılır
        public decimal AvailableBalance { get; set; } // kullanılabilir bakiye
        public DateTime AddedDate { get; set; } // Kartın sisteme eklendiği tarih

        public ICollection<Payment> Payments { get; set; }
    }
}
