using BusinessLogicLayer.ProductService;
using BusinessLogicLayer.ProductService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        Logger.Info("CreateProduct endpoint called with data: {Data}", request);

        var result = await productService.ProductCreateAsync(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Product created successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("CreateProduct failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        Logger.Info("DeleteProduct endpoint called for product: {Id}", id);

        var result = await productService.ProductDeleteAsync(id);

        if (result.IsSuccessful)
        {
            Logger.Info("Product deleted successfully for product: {Id}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("DeleteProduct failed for product: {Id}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> Get()
    {
        Logger.Info("GetAllProducts endpoint called");

        var result = await productService.ProductGetAllAsync();

        if (result.IsSuccessful)
        {
            Logger.Info("Products retrieved successfully");
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetAllProducts failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> Get(Guid id)
    {
        Logger.Info("GetProductById endpoint called for product: {Id}", id);

        var result = await productService.ProductGetByIdAsync(id);

        if (result.IsSuccessful)
        {
            Logger.Info("Product retrieved successfully for product: {Id}", id);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("GetProductById failed for product: {Id}, Error: {Error}", id, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(ProductUpdateRequest request)
    {
        Logger.Info("UpdateProduct endpoint called with data: {Data}", request);

        var result = await productService.ProductUpdateAsync(request);

        if (result.IsSuccessful)
        {
            Logger.Info("Product updated successfully with data: {Data}", request);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("UpdateProduct failed, Error: {Error}", result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }

    [HttpGet("Filter/{categoryId}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> Filter(Guid categoryId)
    {
        Logger.Info("FilterProductsByCategory endpoint called for category: {CategoryId}", categoryId);

        var result = await productService.ProductFilterCategoryAsync(categoryId);

        if (result.IsSuccessful)
        {
            Logger.Info("Products filtered successfully for category: {CategoryId}", categoryId);
            return Ok(result.Data);
        }
        else
        {
            Logger.Warn("FilterProductsByCategory failed for category: {CategoryId}, Error: {Error}", categoryId, result.ErrorMessage);
            return StatusCode((int)result.StatusCode, result.ErrorMessage);
        }
    }
}
