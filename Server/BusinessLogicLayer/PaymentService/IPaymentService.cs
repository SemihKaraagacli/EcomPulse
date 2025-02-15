using BusinessLogicLayer.PaymentService.Dtos;

namespace BusinessLogicLayer.PaymentService;

public interface IPaymentService
{
    Task<ServiceResult<PaymentResponse>> PaymnetProcessing(PaymentCreateRequest request);
}
