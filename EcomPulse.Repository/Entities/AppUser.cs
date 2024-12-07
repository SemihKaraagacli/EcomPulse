using Microsoft.AspNetCore.Identity;

namespace EcomPulse.Repository.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public ICollection<Order> Orders { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public ICollection<CreditCard> CreditCards { get; set; }
    }
}
