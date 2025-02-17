using BusinessLogicLayer.ProductService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.Entities;
using DataAccessLayer.ProductRepository;
using System.Net;

namespace BusinessLogicLayer.ProductService;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork) : IProductService
{
    public async Task<Result<string>> ProductCreateAsync(ProductCreateRequest request)
    {
        var hasProduct = await productRepository.WhereAsync(x => x.Name == request.Name);
        if (hasProduct.Any())
        {
            return Result<string>.Failure(HttpStatusCode.BadRequest, "Found an order pending processing");
        }
        var newProduct = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Stock = request.Stock,
            CategoryId = request.CategoryId,
        };
        productRepository.Create(newProduct);
        await unitOfWork.CommitAsync();
        return "Ürün başarıyla oluşturuldu";
    }

    public async Task<Result<string>> ProductDeleteAsync(Guid id)
    {
        var hasProduct = await productRepository.GetByIdAsync(id);
        if (hasProduct is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Found an order pending processing");
        }
        productRepository.Delete(hasProduct);
        await unitOfWork.CommitAsync();
        return "Ürün başarıyla silindi";
    }

    public async Task<Result<IEnumerable<ProductResponse>>> ProductFilterCategoryAsync(Guid categoryId)
    {
        var filterProduct = await productRepository.GetFilterProductsAsync(categoryId);
        var productResponse = filterProduct.Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price, x.Stock, x.Category!.Name));
        return Result<IEnumerable<ProductResponse>>.Successful(productResponse);
    }

    public async Task<Result<IEnumerable<ProductResponse>>> ProductGetAllAsync()
    {
        var allProducts = await productRepository.GetAllAsync();
        var productResponse = allProducts.Select(x => new ProductResponse(x.Id, x.Name, x.Description, x.Price, x.Stock, x.Category!.Name));
        return Result<IEnumerable<ProductResponse>>.Successful(productResponse);
    }

    public async Task<Result<ProductResponse>> ProductGetByIdAsync(Guid id)
    {
        var hasProduct = await productRepository.GetByIdAsync(id);
        if (hasProduct is null)
        {
            return Result<ProductResponse>.Failure(HttpStatusCode.NotFound, "Product not found.");
        }
        var productResponse = new ProductResponse(hasProduct.Id, hasProduct.Name, hasProduct.Description, hasProduct.Price, hasProduct.Stock, hasProduct.Category!.Name);
        return productResponse;
    }

    public async Task<Result<string>> ProductUpdateAsync(ProductUpdateRequest request)
    {
        var hasProduct = await productRepository.GetByIdAsync(request.Id);
        if (hasProduct is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Found an order pending processing");
        }
        hasProduct.Name = request.Name;
        hasProduct.Description = request.Description;
        hasProduct.Price = request.Price;
        hasProduct.Stock = request.Stock;
        hasProduct.CategoryId = request.CategoryId;
        productRepository.Update(hasProduct);
        await unitOfWork.CommitAsync();
        return "Ürün başarıyla güncellendi";
    }
}
