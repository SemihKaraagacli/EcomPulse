using BusinessLogicLayer.ProductService.Dtos;

namespace BusinessLogicLayer.ProductService;

public interface IProductService
{
    Task<ServiceResult> ProductCreateAsync(ProductCreateRequest request);
    Task<ServiceResult> ProductUpdateAsync(ProductUpdateRequest request);
    Task<ServiceResult> ProductDeleteAsync(Guid id);
    Task<ServiceResult<IEnumerable<ProductResponse>>> ProductGetAllAsync();
    Task<ServiceResult<IEnumerable<ProductResponse>>> ProductFilterCategoryAsync(Guid categoryId);
    Task<ServiceResult<ProductResponse>> ProductGetByIdAsync(Guid id);
}
