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
    }
}
