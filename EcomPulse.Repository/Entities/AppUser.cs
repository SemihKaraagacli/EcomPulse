using Microsoft.AspNetCore.Identity;

namespace EcomPulse.Repository.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
    }
}
