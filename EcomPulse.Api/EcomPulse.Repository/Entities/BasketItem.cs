namespace EcomPulse.Repository.Entities
{
    public class BasketItem
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public Basket Basket { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}