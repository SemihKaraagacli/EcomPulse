using EcomPulse.Repository.CategoryRepository;
using EcomPulse.Repository.Entities;
using EcomPulse.Service.CategoryService.Dtos;
using EcomPulse.Service.UnitOfWork;
using System.Net;

namespace EcomPulse.Service.CategoryService
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    {
        public async Task<ServiceResult> CategoryAdd(CategoryCreateRequest request)
        {
            var hasCategory = await categoryRepository.WhereAsync(x => x.Name == request.Name);
            if (hasCategory != null)
            {
                return ServiceResult.Fail("Category already exists", HttpStatusCode.BadRequest);
            }
            var newCategory = new Category
            {
                Name = request.Name,
            };
            categoryRepository.CreateAsync(newCategory);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult> CategoryUpdate(CategoryUpdateRequest request)
        {
            var hasCategory = await categoryRepository.GetByIdAsync(request.Id);
            if (hasCategory is null)
            {
                return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
            }
            hasCategory.Name = request.Name;
            categoryRepository.UpdateAsync(hasCategory);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult> CategoryDelete(Guid id)
        {
            var hasCategory = await categoryRepository.GetByIdAsync(id);
            if (hasCategory is null)
            {
                return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
            }
            categoryRepository.DeleteAsync(hasCategory);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<IEnumerable<CategoryResponse>>> GetAllAsync()
        {
            var all = await categoryRepository.GetAllAsync();
            var categoryResponse = all.Select(x => new CategoryResponse(x.Id, x.Name));
            return ServiceResult<IEnumerable<CategoryResponse>>.Success(categoryResponse, HttpStatusCode.OK);
        }
    }
}
