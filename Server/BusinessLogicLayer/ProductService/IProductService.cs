using BusinessLogicLayer.ProductService.Dtos;

namespace BusinessLogicLayer.ProductService;

public interface IProductService
{
    Task<Result<string>> ProductCreateAsync(ProductCreateRequest request);
    Task<Result<string>> ProductUpdateAsync(ProductUpdateRequest request);
    Task<Result<string>> ProductDeleteAsync(Guid id);
    Task<Result<IEnumerable<ProductResponse>>> ProductGetAllAsync();
    Task<Result<IEnumerable<ProductResponse>>> ProductFilterCategoryAsync(Guid categoryId);
    Task<Result<ProductResponse>> ProductGetByIdAsync(Guid id);
}
