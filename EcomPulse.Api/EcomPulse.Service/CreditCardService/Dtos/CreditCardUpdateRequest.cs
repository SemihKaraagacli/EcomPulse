namespace EcomPulse.Service.CreditCardService.Dtos
{
    public record CreditCardUpdateRequest(Guid Id, decimal AvailableBalance);
}
