using BusinessLogicLayer.CategoryService;
using BusinessLogicLayer.CategoryService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        var result = await categoryService.CategoryCreateAsync(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> GetAll()
    {
        var result = await categoryService.CategoryGetAllAsync();
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await categoryService.CategoryGetByIdAsync(id);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage); ;
    }
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request)
    {
        var result = await categoryService.CategoryUpdateAsync(id, request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await categoryService.CategoryDeleteAsync(id);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }

}
