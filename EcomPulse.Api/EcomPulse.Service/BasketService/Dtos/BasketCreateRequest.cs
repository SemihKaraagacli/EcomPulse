namespace EcomPulse.Service.BasketService.Dtos
{
    public record BasketCreateRequest(Guid UserId, Guid ProductId, int Quantity);
}
