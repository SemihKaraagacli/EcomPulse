using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Entities;

public class AppUser : IdentityUser<Guid>
{
    public string Address { get; set; } = default!;
    public string City { get; set; } = default!;
    public string County { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = default!;
    public ICollection<Basket> Baskets { get; set; } = default!;
    public ICollection<CreditCard> CreditCards { get; set; } = default!;
}
