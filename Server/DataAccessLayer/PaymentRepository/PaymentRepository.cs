using DataAccessLayer.Entities;
using DataAccessLayer.GenericRepository;

namespace DataAccessLayer.PaymentRepository;

public class PaymentRepository(AppDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
{
}
