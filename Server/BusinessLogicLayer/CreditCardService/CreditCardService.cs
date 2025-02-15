using BusinessLogicLayer.CreditCardService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.CreditCardRepository;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessLogicLayer.CreditCardService;

public class CreditCardService(ICreditCardRespository creditCardRespository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : ICreditCardService
{
    public async Task<ServiceResult> CreateAsync(CreditCardCreateRequest request)
    {
        var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
        if (hasUser is null)
        {
            return ServiceResult.Fail("User not found.", HttpStatusCode.NotFound);
        }
        var hasCreditCard = await userManager.Users.Select(x => x.CreditCards).ToListAsync();
        if (hasCreditCard != null && !hasCreditCard.Any())
        {
            return ServiceResult.Fail("Credit card already exists.", HttpStatusCode.BadRequest);
        }
        DateTime parseDate = DateTime.ParseExact(request.ExpirationDate, "MM/yyyy", null);
        var newCreditCard = new CreditCard
        {
            AddedDate = DateTime.Now,
            AvailableBalance = request.AvailableBalance,
            CardHolderName = request.CardHolderName,
            CardNumber = request.CardNumber,
            CVV = request.CVV,
            ExpirationDate = parseDate,
            UserId = request.UserId,
        };
        creditCardRespository.Create(newCreditCard);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success(HttpStatusCode.OK);
    }
    public async Task<ServiceResult> UpdateAsync(CreditCardUpdateRequest request)
    {
        var hasCreditCard = await creditCardRespository.GetById(request.Id);
        if (hasCreditCard is null)
        {
            return ServiceResult.Fail("Credit card not found.", HttpStatusCode.NotFound);
        }
        hasCreditCard.AvailableBalance = request.AvailableBalance;
        creditCardRespository.Update(hasCreditCard);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success(HttpStatusCode.OK);
    }
    public async Task<ServiceResult> DeleteAsync(Guid Id)
    {
        var hasCreditCard = await creditCardRespository.GetById(Id);
        if (hasCreditCard is null)
        {
            return ServiceResult.Fail("Credit card not found.", HttpStatusCode.NotFound);
        }
        creditCardRespository.Delete(hasCreditCard);
        await unitOfWork.CommitAsync();
        return ServiceResult.Success(HttpStatusCode.OK);
    }
}
