namespace EcomPulse.Service.CreditCardService.Dtos
{
    public record CreditCardUpdateRequest(Guid UserId, decimal AvailableBalance);
}
