namespace EcomPulse.Service.ProductService.Dtos
{
    public record ProductUpdateRequest(string Name, string Description, decimal Price, int Stock, Guid CategoryId);
}
