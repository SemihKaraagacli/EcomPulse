namespace BusinessLogicLayer.CreditCardService.Dtos;

public record CreditCardResponse(Guid Id, Guid UserId, string CardHolderName, string CardNumber, string ExpirationDate, string CVV, decimal AvailableBalance);
