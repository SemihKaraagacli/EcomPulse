using DataAccessLayer.Entities;

namespace BusinessLogicLayer.BasketService.Dtos;

public record BasketResponse(Guid Id, Guid UserId, List<BasketItemResponse> BasketItems, decimal TotalPrice);
