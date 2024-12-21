using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.CreditCardRepository
{
    public interface ICreditCardRespository : IGenericRepository<CreditCard>
    {
        Task<CreditCard> GetById(Guid Id);
    }
}
