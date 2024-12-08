using EcomPulse.Service.CategoryService.Dtos;

namespace EcomPulse.Service.CategoryService
{
    public interface ICategoryService
    {
        Task<ServiceResult> CategoryCreateAsync(CategoryCreateRequest request);
        Task<ServiceResult> CategoryDeleteAsync(Guid id);
        Task<ServiceResult<IEnumerable<CategoryResponse>>> CategoryGetAllAsync();
        Task<ServiceResult<CategoryResponse>> CategoryGetByIdAsync(Guid id);
        Task<ServiceResult> CategoryUpdateAsync(CategoryUpdateRequest request);
    }
}