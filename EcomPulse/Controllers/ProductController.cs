using EcomPulse.Service.ProductService;
using EcomPulse.Service.ProductService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EcomPulse.Controllers
{
    public class ProductController(IProductService productService) : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            var result = await productService.ProductCreateAsync(request);
            return CreateObjectResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await productService.ProductDeleteAsync(id);
            return CreateObjectResult(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await productService.ProductGetAllAsync();
            return CreateObjectResult(result);
        }

    }
}
