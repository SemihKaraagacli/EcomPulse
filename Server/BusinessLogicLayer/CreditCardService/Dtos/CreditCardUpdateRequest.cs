namespace BusinessLogicLayer.CreditCardService.Dtos;

public record CreditCardUpdateRequest(Guid Id, decimal AvailableBalance);
