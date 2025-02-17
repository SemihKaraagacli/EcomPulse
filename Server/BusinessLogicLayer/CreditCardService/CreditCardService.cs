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
    public async Task<Result<string>> CreateAsync(CreditCardCreateRequest request)
    {
        var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User not found.");
        }
        var hasCreditCard = await userManager.Users.Select(x => x.CreditCards).ToListAsync();
        if (hasCreditCard != null && !hasCreditCard.Any())
        {
            return Result<string>.Failure(HttpStatusCode.BadRequest, "Credit card already exists.");
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
        return "Kredi kartı başarıyla oluşturuldu";
    }
    public async Task<Result<string>> UpdateAsync(CreditCardUpdateRequest request)
    {
        var hasCreditCard = await creditCardRespository.GetById(request.Id);
        if (hasCreditCard is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Credit card not found.");
        }
        hasCreditCard.AvailableBalance = request.AvailableBalance;
        creditCardRespository.Update(hasCreditCard);
        await unitOfWork.CommitAsync();
        return "Kredi kartı başarıyla güncellendi";
    }
    public async Task<Result<string>> DeleteAsync(Guid Id)
    {
        var hasCreditCard = await creditCardRespository.GetById(Id);
        if (hasCreditCard is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Credit card not found.");
        }
        creditCardRespository.Delete(hasCreditCard);
        await unitOfWork.CommitAsync();
        return "Kredi kartı başarıyla silindi";
    }
}
