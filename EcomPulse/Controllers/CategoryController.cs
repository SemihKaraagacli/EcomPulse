using EcomPulse.Service.CategoryService;
using EcomPulse.Service.CategoryService.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{,
    public class CategoryController(ICategoryService categoryService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequest request)
        {
            var result = await categoryService.CategoryCreateAsync(request);
            return CreateObjectResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await categoryService.CategoryGetAllAsync();
            return CreateObjectResult(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await categoryService.CategoryGetByIdAsync(id);
            return CreateObjectResult(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request)
        {
            var result = await categoryService.CategoryUpdateAsync(id, request);
            return CreateObjectResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await categoryService.CategoryDeleteAsync(id);
            return CreateObjectResult(result);
        }

    }
}
