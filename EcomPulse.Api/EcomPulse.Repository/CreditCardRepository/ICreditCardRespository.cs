using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.CreditCardRepository
{
    public interface ICreditCardRespository : IGenericRepository<CreditCard>
    {
        Task<CreditCard> GetById(Guid Id);
        Task<List<CreditCard>> GetExpiredCardsAsync(CancellationToken cancellationToken);
        Task DeleteExpiredCardsAsync(List<CreditCard> expiredCards, CancellationToken cancellationToken);
    }
}
