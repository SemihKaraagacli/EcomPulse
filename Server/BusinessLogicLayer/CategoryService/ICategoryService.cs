using BusinessLogicLayer.CategoryService.Dtos;

namespace BusinessLogicLayer.CategoryService;

public interface ICategoryService
{
    Task<Result<string>> CategoryCreateAsync(CategoryCreateRequest request);
    Task<Result<string>> CategoryDeleteAsync(Guid id);
    Task<Result<IEnumerable<CategoryResponse>>> CategoryGetAllAsync();
    Task<Result<CategoryResponse>> CategoryGetByIdAsync(Guid id);
    Task<Result<string>> CategoryUpdateAsync(Guid Id, CategoryUpdateRequest request);
}