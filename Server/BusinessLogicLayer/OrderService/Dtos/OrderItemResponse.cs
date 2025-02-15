namespace BusinessLogicLayer.OrderService.Dtos;

public record OrderItemResponse(Guid Id, Guid ProductId, int Quantity, decimal TotalPrice, decimal UnitPrice);
