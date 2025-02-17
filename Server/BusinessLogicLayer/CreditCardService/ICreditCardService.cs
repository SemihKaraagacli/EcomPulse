using BusinessLogicLayer.CreditCardService.Dtos;

namespace BusinessLogicLayer.CreditCardService;

public interface ICreditCardService
{
    Task<Result<string>> CreateAsync(CreditCardCreateRequest request);
    Task<Result<string>> UpdateAsync(CreditCardUpdateRequest request);
    Task<Result<string>> DeleteAsync(Guid Id);
}