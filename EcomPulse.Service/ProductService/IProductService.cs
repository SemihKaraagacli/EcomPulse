using EcomPulse.Service.ProductService.Dtos;

namespace EcomPulse.Service.ProductService
{
    public interface IProductService
    {
        Task<ServiceResult> ProductCreateAsync(ProductCreateRequest request);
    }
}
