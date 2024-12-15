using EcomPulse.Repository.BasketItemRepository;
using EcomPulse.Repository.BasketRepository;
using EcomPulse.Repository.Entities;
using EcomPulse.Repository.ProductRepository;
using EcomPulse.Service.BasketService.Dtos;
using EcomPulse.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EcomPulse.Service.BasketService
{
    public class BasketService(UserManager<AppUser> userManager, IProductRepository productRepository, IBasketItemRepository basketItemRepository, IBasketRepository basketRepository, IUnitOfWork unitOfWork) : IBasketService
    {
        public async Task<ServiceResult> CreateBasket(BasketCreateRequest request)
        {
            var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
            if (hasUser is null)
            {
                return ServiceResult.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var hasProduct = await productRepository.GetByIdAsync(request.ProductId);
            if (hasProduct is null)
            {
                return ServiceResult.Fail("Product not found.", HttpStatusCode.NotFound);
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
                basketRepository.CreateAsync(newBasket);
                basketItemRepository.CreateAsync(newBasketItem);
            }
            else
            {
                var hasBasketItem = hasBasket.BasketItems.FirstOrDefault(bi => bi.ProductId == request.ProductId);
                if (hasBasketItem is null) // If the cart item does not exist, create a new cart item. Match the items.
                {
                    var newBasketItem = new BasketItem
                    {
                        ProductId = hasProduct.Id,
                        Price = hasProduct.Price,
                        Quantity = request.Quantity,
                    };
                    hasBasket.BasketItems.Add(newBasketItem);
                    basketItemRepository.CreateAsync(newBasketItem);
                }
                else // if there is a basket and a basket item, equalize the incoming quantity with the quantity in the basket item and delete the basket item if 0.
                {
                    hasBasketItem.Quantity = request.Quantity;
                    basketItemRepository.UpdateAsync(hasBasketItem);
                    if (hasBasketItem.Quantity == 0)  
                    {
                        basketItemRepository.DeleteAsync(hasBasketItem);
                    }
                }
            }
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<BasketResponse>> GetBasket(Guid userId)
        {
            var hasUser = await userManager.FindByIdAsync(userId.ToString());
            if (hasUser == null)
            {
                return ServiceResult<BasketResponse>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var basket = await basketRepository.GetAllAsync(userId);
            var basketItemResponse = basket.BasketItems.Select(x => new BasketItemResponse(x.Id, x.ProductId, x.Product.Name, x.Quantity, x.Product.Price));
            var basketResponse = new BasketResponse(basket.Id, userId, basketItemResponse.ToList(), basket.TotalPrice);
            return ServiceResult<BasketResponse>.Success(basketResponse, HttpStatusCode.OK);
        }

    }
}
