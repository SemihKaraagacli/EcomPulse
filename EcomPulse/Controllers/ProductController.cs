using EcomPulse.Service.ProductService;
using EcomPulse.Service.ProductService.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class ProductController(IProductService productService) : CustomControllerBase
    {

        [HttpPost]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var result = await productService.ProductCreateAsync(request);
            return CreateObjectResult(result);
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.ProductDeleteAsync(id);
            return CreateObjectResult(result);
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            var result = await productService.ProductGetAllAsync();
            return CreateObjectResult(result);
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await productService.ProductGetByIdAsync(id);
            return CreateObjectResult(result);
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = "SigninToken")]
        public async Task<IActionResult> Update(ProductUpdateRequest request)
        {
            var result = await productService.ProductUpdateAsync(request);
            return CreateObjectResult(result);
        }
        [HttpGet("/Filter/{categoryId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Filter(Guid categoryId)
        {
            var result = await productService.ProductFilterCategoryAsync(categoryId);
            return CreateObjectResult(result);
        }
    }
}
