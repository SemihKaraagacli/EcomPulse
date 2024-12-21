using EcomPulse.Service.CreditCardService.Dtos;

namespace EcomPulse.Service.CreditCardService
{
    public interface ICreditCardService
    {
        Task<ServiceResult> CreateAsync(CreditCardCreateRequest request);
        Task<ServiceResult> UpdateAsync(CreditCardUpdateRequest request);
        Task<ServiceResult> DeleteAsync(Guid Id);
    }
}