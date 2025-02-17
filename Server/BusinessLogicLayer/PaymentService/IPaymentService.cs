using BusinessLogicLayer.PaymentService.Dtos;

namespace BusinessLogicLayer.PaymentService;

public interface IPaymentService
{
    Task<Result<PaymentResponse>> PaymnetProcessing(PaymentCreateRequest request);
}
