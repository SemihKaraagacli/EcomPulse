namespace EcomPulse.Service.CreditCardService.Dtos
{
    public record CreditCardResponse(Guid UserId, string CardHolderName, string CardNumber, string ExpirationDate, string CVV, decimal AvailableBalance);
}
