using BusinessLogicLayer.CategoryService;
using BusinessLogicLayer.CategoryService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

public class CategoryController(ICategoryService categoryService, ILogger<CustomControllerBase> logger) : CustomControllerBase(logger)
{
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        var result = await categoryService.CategoryCreateAsync(request);
        return CreateObjectResult(result);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> GetAll()
    {
        var result = await categoryService.CategoryGetAllAsync();
        return CreateObjectResult(result);
    }
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await categoryService.CategoryGetByIdAsync(id);
        return CreateObjectResult(result);
    }
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request)
    {
        var result = await categoryService.CategoryUpdateAsync(id, request);
        return CreateObjectResult(result);
    }
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await categoryService.CategoryDeleteAsync(id);
        return CreateObjectResult(result);
    }

}
