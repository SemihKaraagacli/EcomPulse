using EcomPulse.Service.PaymentService.Dtos;

namespace EcomPulse.Service.PaymentService
{
    public interface IPaymentService
    {
        Task<ServiceResult<PaymentResponse>> PaymnetProcessing(PaymentCreateRequest request);
    }
}
