﻿using EcomPulse.Repository.BasketItemRepository;
using EcomPulse.Repository.BasketRepository;
using EcomPulse.Repository.Entities;
using EcomPulse.Repository.ProductRepository;
using EcomPulse.Service.BasketService.Dtos;
using EcomPulse.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EcomPulse.Service.BasketService
{
    public class BasketService(UserManager<AppUser> userManager, IProductRepository productRepository, IBasketItemRepository basketItemRepository, BasketRepository basketRepository, IUnitOfWork unitOfWork)
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
            if (hasBasket is null)
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
                var updateStock = hasProduct.Stock - request.Quantity;
                hasProduct.Stock = updateStock;
                newBasket.BasketItems.Add(newBasketItem);
                productRepository.UpdateAsync(hasProduct);
                basketRepository.CreateAsync(newBasket);
                basketItemRepository.CreateAsync(newBasketItem);
            }
            else
            {
                if (request.Quantity > 0)
                {
                    var hasBasketItem = hasBasket.BasketItems.FirstOrDefault(bi => bi.ProductId == request.ProductId);
                    if (hasBasketItem == null)
                    {
                        return ServiceResult.Fail("Basket Item not found.", HttpStatusCode.NotFound);

                    }
                    hasBasketItem.Quantity += request.Quantity;
                    basketItemRepository.UpdateAsync(hasBasketItem);
                }
                if (request.Quantity < 0)
                {
                    var hasBasketItem = hasBasket.BasketItems.FirstOrDefault(bi => bi.ProductId == request.ProductId);
                    if (hasBasketItem == null)
                    {
                        return ServiceResult.Fail("Basket Item not found.", HttpStatusCode.NotFound);

                    }
                    hasBasketItem.Quantity -= request.Quantity;
                    basketItemRepository.UpdateAsync(hasBasketItem);
                }
            }
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
    }
}
