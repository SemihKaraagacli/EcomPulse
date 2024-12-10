using EcomPulse.Service.CategoryService;
using EcomPulse.Service.CategoryService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class CategoryController(ICategoryService categoryService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CategoryCreate(CategoryCreateRequest request)
        {
            var result = await categoryService.CategoryCreateAsync(request);
            return CreateObjectResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> CategoryGetAll()
        {
            var result = await categoryService.CategoryGetAllAsync();
            return CreateObjectResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> CategoryGetById(Guid id)
        {
            var result = await categoryService.CategoryGetByIdAsync(id);
            return CreateObjectResult(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> CategoryUpdate(Guid id, CategoryUpdateRequest request)
        {
            var result = await categoryService.CategoryUpdateAsync(id, request);
            return CreateObjectResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> CategoryDelete(Guid id)
        {
            var result = await categoryService.CategoryDeleteAsync(id);
            return CreateObjectResult(result);
        }

    }
}
