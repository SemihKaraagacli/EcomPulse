using BusinessLogicLayer.CreditCardService.Dtos;

namespace BusinessLogicLayer.CreditCardService;

public interface ICreditCardService
{
    Task<ServiceResult> CreateAsync(CreditCardCreateRequest request);
    Task<ServiceResult> UpdateAsync(CreditCardUpdateRequest request);
    Task<ServiceResult> DeleteAsync(Guid Id);
}