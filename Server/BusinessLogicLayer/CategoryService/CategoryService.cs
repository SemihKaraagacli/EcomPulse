using BusinessLogicLayer.CategoryService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.CategoryRepository;
using DataAccessLayer.Entities;
using System.Net;

namespace BusinessLogicLayer.CategoryService;

public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) : ICategoryService
{
    public async Task<Result<string>> CategoryCreateAsync(CategoryCreateRequest request)
    {
        var hasCategory = await categoryRepository.WhereAsync(x => x.Name == request.Name);
        if (hasCategory.Any())
        {
            return Result<string>.Failure(HttpStatusCode.BadRequest, "Category already exists");
        }
        var newCategory = new Category
        {
            Name = request.Name,
        };
        categoryRepository.Create(newCategory);
        await unitOfWork.CommitAsync();
        return "Kategori başarıyla oluşyuruldu";
    }
    public async Task<Result<string>> CategoryUpdateAsync(Guid Id, CategoryUpdateRequest request)
    {
        var hasCategory = await categoryRepository.GetByIdAsync(Id);
        if (hasCategory is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Category not found");
        }
        hasCategory.Name = request.Name;
        categoryRepository.Update(hasCategory);
        await unitOfWork.CommitAsync();
        return "Kategori başarıyla güncellendi";
    }
    public async Task<Result<string>> CategoryDeleteAsync(Guid id)
    {
        var hasCategory = await categoryRepository.GetByIdAsync(id);
        if (hasCategory is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Category not found");
        }
        categoryRepository.Delete(hasCategory);
        await unitOfWork.CommitAsync();
        return "Kategori başarıyla silindi";
    }
    public async Task<Result<IEnumerable<CategoryResponse>>> CategoryGetAllAsync()
    {
        var all = await categoryRepository.GetAllAsync();
        var categoryResponse = all.Select(x => new CategoryResponse(x.Id, x.Name)).ToList();
        return categoryResponse;
    }
    public async Task<Result<CategoryResponse>> CategoryGetByIdAsync(Guid id)
    {
        var hasCategory = await categoryRepository.GetByIdAsync(id);
        if (hasCategory is null)
        {
            return Result<CategoryResponse>.Failure(HttpStatusCode.NotFound, "Category not found.");
        }
        var categoryResponse = new CategoryResponse(hasCategory.Id, hasCategory.Name);
        return categoryResponse;
    }
}
