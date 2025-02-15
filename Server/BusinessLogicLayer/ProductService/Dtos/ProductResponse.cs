using DataAccessLayer.Entities;

namespace BusinessLogicLayer.ProductService.Dtos;

public record ProductResponse(Guid Id, string Name, string Description, decimal Price, int Stock, string CategoryName);
