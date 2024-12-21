using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.CreditCardRepository
{
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRespository
    {
        private readonly AppDbContext _context;
        public CreditCardRepository(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<CreditCard> GetById(Guid Id)
        {
            return await _context.CreditCards.FirstOrDefaultAsync(x=>x.Id==Id);
        }
    }
}
