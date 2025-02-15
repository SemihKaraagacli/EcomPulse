namespace BusinessLogicLayer.OrderService.Dtos;

public record OrderResponse(Guid Id, Guid UserId, List<OrderItemResponse> OrderItems, DateTime CreatedAt, decimal TotalAmount, string OrderStatus);
