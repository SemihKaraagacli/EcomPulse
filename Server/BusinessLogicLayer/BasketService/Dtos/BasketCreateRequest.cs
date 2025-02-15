namespace BusinessLogicLayer.BasketService.Dtos;

public record BasketCreateRequest(Guid UserId, Guid ProductId, int Quantity);
