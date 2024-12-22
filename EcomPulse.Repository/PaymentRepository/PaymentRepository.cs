using EcomPulse.Repository.Entities;
using EcomPulse.Repository.GenericRepository;

namespace EcomPulse.Repository.PaymentRepository
{
    public class PaymentRepository(AppDbContext context) : GenericRepository<Payment>(context), IPaymentRepository
    {
    }
}
