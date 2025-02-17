using BusinessLogicLayer.ProductService;
using BusinessLogicLayer.ProductService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{

    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var result = await productService.ProductCreateAsync(request);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await productService.ProductDeleteAsync(id);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> Get()
    {
        var result = await productService.ProductGetAllAsync();
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("{id}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await productService.ProductGetByIdAsync(id);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Update(ProductUpdateRequest request)
    {
        var result = await productService.ProductUpdateAsync(request);
        return result.IsSuccessful
              ? Ok(result)
              : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
    [HttpGet("Filter/{categoryId}")]
    [Authorize(AuthenticationSchemes = "Client_Token")]
    public async Task<IActionResult> Filter(Guid categoryId)
    {
        var result = await productService.ProductFilterCategoryAsync(categoryId);
        return result.IsSuccessful
            ? Ok(result)
            : StatusCode((int)result.StatusCode, result.ErrorMessage);
    }
}
