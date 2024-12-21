using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.CreditCardRepository
{
    public class CreditCardRepository(AppDbContext context) : GenericRepository<CreditCard>(context), ICreditCardRespository
    {
    }
}
