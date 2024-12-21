namespace EcomPulse.Service.CreditCardService.Dtos
{
    public record CreditCardResponse(Guid Id, Guid UserId, string CardHolderName, string CardNumber, string ExpirationDate, string CVV, decimal AvailableBalance);
}
