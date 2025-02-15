using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.PaymentRepository;

public interface IPaymentRepository : IGenericRepository<Payment>
{
}
