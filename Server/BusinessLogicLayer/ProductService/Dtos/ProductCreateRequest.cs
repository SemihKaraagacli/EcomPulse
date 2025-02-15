namespace BusinessLogicLayer.ProductService.Dtos;

public record ProductCreateRequest(string Name, string Description, decimal Price, int Stock, Guid CategoryId);
