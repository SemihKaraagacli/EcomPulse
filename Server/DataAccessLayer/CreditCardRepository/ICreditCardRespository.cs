using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.CreditCardRepository;

public interface ICreditCardRespository : IGenericRepository<CreditCard>
{
    Task<CreditCard> GetById(Guid Id);
    Task<List<CreditCard>> GetExpiredCardsAsync(CancellationToken cancellationToken);
    Task DeleteExpiredCardsAsync(List<CreditCard> expiredCards, CancellationToken cancellationToken);
}
