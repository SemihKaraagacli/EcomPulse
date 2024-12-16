﻿namespace EcomPulse.Service.BasketService.Dtos
{
    public record BasketItemResponse(Guid Id, Guid ProductId, string ProductName, int Quantity, decimal ProductPrice);
}