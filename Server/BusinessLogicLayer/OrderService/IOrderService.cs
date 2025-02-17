using BusinessLogicLayer.OrderService.Dtos;

namespace BusinessLogicLayer.OrderService;

public interface IOrderService
{
    Task<Result<string>> CreateOrder(OrderCreateRequest request);
    Task<Result<IEnumerable<OrderResponse>>> GetAllOrder(Guid userId);
    Task<Result<OrderResponse>> GetByIdOrder(Guid OrderId);
    Task<Result<string>> OrderDelete(Guid orderId);
}