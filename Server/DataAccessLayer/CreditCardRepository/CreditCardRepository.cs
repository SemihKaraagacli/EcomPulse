using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.CreditCardRepository;

public class CreditCardRepository : GenericRepository<CreditCard>, ICreditCardRespository
{
    private readonly AppDbContext _context;
    public CreditCardRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CreditCard> GetById(Guid Id)
    {
        var response = await _context.CreditCards.FirstOrDefaultAsync(x => x.Id == Id);
        return response!;
    }
    public async Task<List<CreditCard>> GetExpiredCardsAsync(CancellationToken cancellationToken)
    {
        var response = await _context.CreditCards
            .Where(card => card.ExpirationDate < DateTime.Now)
            .ToListAsync(cancellationToken);
        return response;
    }

    public async Task DeleteExpiredCardsAsync(List<CreditCard> expiredCards, CancellationToken cancellationToken)
    {
        _context.CreditCards.RemoveRange(expiredCards);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
