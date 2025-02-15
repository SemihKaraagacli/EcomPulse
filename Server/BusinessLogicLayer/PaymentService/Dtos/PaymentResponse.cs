namespace BusinessLogicLayer.PaymentService.Dtos;

public record PaymentResponse(Guid Id, Guid OrderId, decimal Amount, DateTime PaymentDate, string PaymentMethod, string PaymentStatus, Guid? CreditCardId);
