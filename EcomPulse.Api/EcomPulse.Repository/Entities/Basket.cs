﻿namespace EcomPulse.Repository.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
        public decimal TotalPrice => BasketItems.Sum(item => item.Quantity * item.Price); // Dinamik toplam fiyat
    }
}
