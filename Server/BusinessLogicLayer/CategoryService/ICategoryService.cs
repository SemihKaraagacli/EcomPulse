using BusinessLogicLayer.CategoryService.Dtos;

namespace BusinessLogicLayer.CategoryService;

public interface ICategoryService
{
    Task<ServiceResult> CategoryCreateAsync(CategoryCreateRequest request);
    Task<ServiceResult> CategoryDeleteAsync(Guid id);
    Task<ServiceResult<IEnumerable<CategoryResponse>>> CategoryGetAllAsync();
    Task<ServiceResult<CategoryResponse>> CategoryGetByIdAsync(Guid id);
    Task<ServiceResult> CategoryUpdateAsync(Guid Id, CategoryUpdateRequest request);
}