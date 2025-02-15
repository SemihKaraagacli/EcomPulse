using BusinessLogicLayer.CategoryService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.CategoryRepository;
using DataAccessLayer.Entities;
using System.Net;

namespace BusinessLogicLayer.CategoryService;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<ServiceResult> CategoryCreateAsync(CategoryCreateRequest request)
    {
        var hasCategory = await categoryRepository.WhereAsync(x => x.Name == request.Name);
        if (hasCategory.Any())
        {
            return ServiceResult.Fail("Category already exists", HttpStatusCode.BadRequest);
        }
        var newCategory = new Category
        {
            Name = request.Name,
        };
        categoryRepository.Create(newCategory);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success(HttpStatusCode.OK);
    }
    public async Task<ServiceResult> CategoryUpdateAsync(Guid Id, CategoryUpdateRequest request)
    {
        var hasCategory = await categoryRepository.GetByIdAsync(Id);
        if (hasCategory is null)
        {
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
        }
        hasCategory.Name = request.Name;
        categoryRepository.Update(hasCategory);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success(HttpStatusCode.OK);
    }
    public async Task<ServiceResult> CategoryDeleteAsync(Guid id)
    {
        var hasCategory = await categoryRepository.GetByIdAsync(id);
        if (hasCategory is null)
        {
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
        }
        categoryRepository.Delete(hasCategory);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success(HttpStatusCode.OK);
    }
    public async Task<ServiceResult<IEnumerable<CategoryResponse>>> CategoryGetAllAsync()
    {
        var all = await categoryRepository.GetAllAsync();
        var categoryResponse = all.Select(x => new CategoryResponse(x.Id, x.Name)).ToList();
        return ServiceResult<IEnumerable<CategoryResponse>>.Success(categoryResponse, HttpStatusCode.OK);
    }
    public async Task<ServiceResult<CategoryResponse>> CategoryGetByIdAsync(Guid id)
    {
        var hasCategory = await categoryRepository.GetByIdAsync(id);
        if (hasCategory is null)
        {
            return ServiceResult<CategoryResponse>.Fail("Category not found.", HttpStatusCode.NotFound);
        }
        var categoryResponse = new CategoryResponse(hasCategory.Id, hasCategory.Name);
        return ServiceResult<CategoryResponse>.Success(categoryResponse, HttpStatusCode.OK);
    }
}
