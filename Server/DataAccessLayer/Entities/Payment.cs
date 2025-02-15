namespace DataAccessLayer.Entities;

public class Payment
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Order? Order { get; set; }

    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PaymentMethod { get; set; } = default!;
    public string PaymentStatus { get; set; } = default!;

    public Guid? CreditCardId { get; set; }
    public CreditCard? CreditCard { get; set; }
}
