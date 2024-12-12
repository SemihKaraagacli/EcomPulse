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

        public async Task<ServiceResult> ProductDeleteAsync(Guid id)
        {
            var hasProduct = await productRepository.GetByIdAsync(id);
            if (hasProduct is null)
            {
                return ServiceResult.Fail("Product not found.", HttpStatusCode.NotFound);
            }
            productRepository.DeleteAsync(hasProduct);
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }

        public Task<ServiceResult<IEnumerable<ProductResponse>>> ProductFilterCategoryAsync(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<ProductResponse>>> ProductGetAllAsync()
        {
            var allProducts = await productRepository.GetAllAsync();
            var productResponse = allProducts.Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price, x.Stock, x.Category.Name));
            return ServiceResult<IEnumerable<ProductResponse>>.Success(productResponse, HttpStatusCode.OK);
        }

        public Task<ServiceResult<ProductResponse>> ProductGetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> ProductUpdateAsync(ProductUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
