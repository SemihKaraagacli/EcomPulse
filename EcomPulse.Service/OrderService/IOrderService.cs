using EcomPulse.Service.OrderService.Dtos;

namespace EcomPulse.Service.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResult> CreateOrder(OrderCreateRequest request);
    }
}