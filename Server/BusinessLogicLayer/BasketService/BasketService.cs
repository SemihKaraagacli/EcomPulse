using BusinessLogicLayer.BasketService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.BasketItemRepository;
using DataAccessLayer.BasketRepository;
using DataAccessLayer.Entities;
using DataAccessLayer.ProductRepository;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BusinessLogicLayer.BasketService;

public class BasketService(UserManager<AppUser> userManager, IProductRepository productRepository, IBasketItemRepository basketItemRepository, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IBasketService
{
    public async Task<Result<string>> CreateBasket(BasketCreateRequest request)
    {
        var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User not found");
        }
        var hasProduct = await productRepository.GetByIdAsync(request.ProductId);
        if (hasProduct is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Product not found");
        }
        var hasBasket = await basketRepository.GetAllAsync(request.UserId);
        if (hasBasket is null) // If there is no basket, create a new basket and a new basket item. Match the items.
        {
            var newBasket = new Basket
            {
                UserId = request.UserId,
                BasketItems = new List<BasketItem>(),
            };
            var newBasketItem = new BasketItem
            {
                ProductId = hasProduct.Id,
                Price = hasProduct.Price,
                Quantity = request.Quantity,
            };
            newBasket.BasketItems.Add(newBasketItem);
            basketRepository.Create(newBasket);
            basketItemRepository.Create(newBasketItem);
        }
        else
        {
            var hasBasketItem = hasBasket.BasketItems!.FirstOrDefault(bi => bi.ProductId == request.ProductId);
            if (hasBasketItem is null) // If the cart item does not exist, create a new cart item. Match the items.
            {
                var newBasketItem = new BasketItem
                {
                    ProductId = hasProduct.Id,
                    Price = hasProduct.Price,
                    Quantity = request.Quantity,
                };
                hasBasket.BasketItems!.Add(newBasketItem);
                basketItemRepository.Create(newBasketItem);
            }
            else // if there is a basket and a basket item, equalize the incoming quantity with the quantity in the basket item and delete the basket item if 0.
            {
                hasBasketItem.Quantity = request.Quantity;
                basketItemRepository.Update(hasBasketItem);
                if (hasBasketItem.Quantity == 0)
                {
                    basketItemRepository.Delete(hasBasketItem);
                }
            }
        }
        await unitOfWork.CommitAsync();
        return "Basket başarıyla oluşturuldu";
    }
    public async Task<Result<BasketResponse>> GetBasket(Guid userId)
    {
        var hasUser = await userManager.FindByIdAsync(userId.ToString());
        if (hasUser == null)
        {
            return Result<BasketResponse>.Failure(HttpStatusCode.NotFound, "User not found.");
        }
        var basket = await basketRepository.GetAllAsync(userId);
        if (basket is null)
        {
            return Result<BasketResponse>.Failure(HttpStatusCode.NotFound, "Basket not found.");
        }
        var basketItemResponse = basket.BasketItems!.Select(x => new BasketItemResponse(x.Id, x.ProductId, x.Product!.Name, x.Quantity, x.Product.Price));
        var basketResponse = new BasketResponse(basket.Id, userId, basketItemResponse.ToList(), basket.TotalPrice);
        return basketResponse;
    }
    public async Task<Result<string>> DeleteBasket(Guid userId)
    {
        var hasUser = await userManager.FindByIdAsync(userId.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User not found.");
        }
        var basket = await basketRepository.GetAllAsync(userId);
        if (basket is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Basket not found.");
        }
        basketRepository.Delete(basket);
        await unitOfWork.CommitAsync();
        return "Basket başarıyla silindi";
    }

}
