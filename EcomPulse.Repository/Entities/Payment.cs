namespace EcomPulse.Repository.Entities
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; } // Kredi Kartı, Nakit vs.
        public string PaymentStatus { get; set; }

        public Guid? CreditCardId { get; set; }  // Kredi kartı bilgileri ile ödeme yapılmışsa
        public CreditCard CreditCard { get; set; }
    }
}
