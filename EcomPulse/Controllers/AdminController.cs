using EcomPulse.Service.CategoryService;
using EcomPulse.Service.CategoryService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class AdminController(ICategoryService categoryService) : CustomControllerBase
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

    }
}
