﻿namespace EcomPulse.Service.PaymentService.Dtos
{
    public record PaymentCreateRequest(Guid OrderId, Guid CreditCardId, string PaymentMethod);
}