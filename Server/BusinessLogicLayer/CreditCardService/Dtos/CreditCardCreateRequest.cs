namespace BusinessLogicLayer.CreditCardService.Dtos;

public record CreditCardCreateRequest(Guid UserId, string CardHolderName, string CardNumber, string ExpirationDate, string CVV, decimal AvailableBalance);
