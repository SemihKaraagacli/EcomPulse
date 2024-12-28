using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace EcomPulse.Repository.CreditCardRepository
{
    public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRespository
    {
        private readonly AppDbContext _context;
        public CreditCardRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CreditCard> GetById(Guid Id)
        {
            return await _context.CreditCards.FirstOrDefaultAsync(x => x.Id == Id);
        }
        public async Task<List<CreditCard>> GetExpiredCardsAsync(CancellationToken cancellationToken)
        {
            return await _context.CreditCards
                .Where(card => card.ExpirationDate < DateTime.Now)
                .ToListAsync(cancellationToken);
        }

        public async Task DeleteExpiredCardsAsync(List<CreditCard> expiredCards, CancellationToken cancellationToken)
        {
            _context.CreditCards.RemoveRange(expiredCards);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
