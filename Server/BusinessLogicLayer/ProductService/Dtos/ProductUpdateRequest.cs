namespace BusinessLogicLayer.ProductService.Dtos;

public record ProductUpdateRequest(Guid Id, string Name, string Description, decimal Price, int Stock, Guid CategoryId);
