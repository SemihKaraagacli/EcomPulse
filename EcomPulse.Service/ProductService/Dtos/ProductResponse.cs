using EcomPulse.Repository.Entities;

namespace EcomPulse.Service.ProductService.Dtos
{
    public record ProductResponse(Guid Id, string Name, string Description, decimal Price, int Stock, string CategoryName);
}
