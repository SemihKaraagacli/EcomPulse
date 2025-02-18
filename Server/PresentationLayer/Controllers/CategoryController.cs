using BusinessLogicLayer.CategoryService;
using BusinessLogicLayer.CategoryService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        Logger.Info("CreateCategory endpoint called with data: {Data}", request);

        var result = await categoryService.CategoryCreateAsync(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Category created successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("CreateCategory failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> GetAll()
    {
        Logger.Info("GetAllCategories endpoint called");

        var result = await categoryService.CategoryGetAllAsync();

        if (result.IsSuccessful)
        {
            Logger.Info("Categories retrieved successfully");
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetAllCategories failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> GetById(Guid id)
    {
        Logger.Info("GetCategoryById endpoint called for category: {Id}", id);

        var result = await categoryService.CategoryGetByIdAsync(id);

        if (result.IsSuccessful)
        {
            Logger.Info("Category retrieved successfully for category: {Id}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetCategoryById failed for category: {Id}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(Guid id, CategoryUpdateRequest request)
    {
        Logger.Info("UpdateCategory endpoint called for category: {Id} with data: {Data}", id, request);

        var result = await categoryService.CategoryUpdateAsync(id, request);

        if (result.IsSuccessful)
        {
            Logger.Info("Category updated successfully for category: {Id} with data: {Data}", id, request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("UpdateCategory failed for category: {Id}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        Logger.Info("DeleteCategory endpoint called for category: {Id}", id);

        var result = await categoryService.CategoryDeleteAsync(id);

        if (result.IsSuccessful)
        {
            Logger.Info("Category deleted successfully for category: {Id}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("DeleteCategory failed for category: {Id}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

}
