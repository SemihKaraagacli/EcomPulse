using EcomPulse.Repository.Entities;
using EcomPulse.Repository.ProductRepository;
using EcomPulse.Service.ProductService.Dtos;
using EcomPulse.Service.UnitOfWork;
using System.Net;

namespace EcomPulse.Service.ProductService
{
    public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
    {
        public async Task<ServiceResult> ProductCreateAsync(ProductCreateRequest request)
        {
            var hasProduct = await productRepository.WhereAsync(x => x.Name == request.Name);
            if (hasProduct.Any())
            {
                return ServiceResult.Fail("Product already exists.", HttpStatusCode.BadRequest);
            }
            var newProduct = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
            };

            productRepository.CreateAsync(newProduct);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
    }
}
