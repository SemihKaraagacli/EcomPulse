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





    }
}
