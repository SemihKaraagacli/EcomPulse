using Azure.Core;
using EcomPulse.Repository.BasketItemRepository;
using EcomPulse.Repository.BasketRepository;
using EcomPulse.Repository.Entities;
using EcomPulse.Repository.OrderItemRepository;
using EcomPulse.Repository.OrderRepository;
using EcomPulse.Service;
using EcomPulse.Service.OrderService.Dtos;
using EcomPulse.Service.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EcomPulse.Service.OrderService
{
    public class OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<ServiceResult> CreateOrder(OrderCreateRequest request)
        {
            var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
            if (hasUser is null)
            {
                return ServiceResult.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var hasBasket = await basketRepository.GetAllAsync(request.UserId);
            if (hasBasket is null)
            {
                return ServiceResult.Fail("Basket not found.", HttpStatusCode.NotFound);
            }
            var hasBasketItem = hasBasket.BasketItems;
            if (hasBasketItem is null)
            {
                return ServiceResult.Fail("Basket item not found.", HttpStatusCode.NotFound);
            }
            var hasOrder = await orderRepository.GetAllOrder(request.UserId);
            if (hasOrder != null && hasOrder.Any(x => x.OrderStatus == "Pending"))
            {
                return ServiceResult.Fail("Found an order pending processing.", HttpStatusCode.BadRequest);
            }
            else
            {
                var newOrderItems = hasBasketItem.Select(basketItem => new OrderItem
                {
                    ProductId = basketItem.ProductId,
                    Quantity = basketItem.Quantity,
                    UnitPrice = basketItem.Price,
                    TotalPrice = basketItem.Quantity * basketItem.Price
                }).ToList();
                var newOrder = new Order
                {
                    OrderItems = newOrderItems, // Add OrderItem list
                    OrderStatus = "Pending",
                    TotalAmount = newOrderItems.Sum(item => item.UnitPrice * item.Quantity), // Calculate total amount
                    CreatedAt = DateTime.UtcNow,
                    UserId = request.UserId
                };
                foreach (var orderItem in newOrderItems)
                {
                    orderItemRepository.CreateAsync(orderItem); // Add OrderItem one by one
                }
                foreach (var basketItem in hasBasketItem)
                {
                    basketItemRepository.DeleteAsync(basketItem); // Delete individual BasketItem
                }
                orderRepository.CreateAsync(newOrder);
                basketRepository.DeleteAsync(hasBasket);
            }
            await unitOfWork.CommitAsync();
            return ServiceResult.Success(HttpStatusCode.OK);
        }
        public async Task<ServiceResult<IEnumerable<OrderResponse>>> GetAllOrder(Guid userId)
        {
            var hasUser = await userManager.FindByIdAsync(userId.ToString());
            if (hasUser is null)
            {
                return ServiceResult<IEnumerable<OrderResponse>>.Fail("User not found.", HttpStatusCode.NotFound);
            }
            var allOrder = await orderRepository.GetAllOrder(userId);
            var orderResponse = allOrder.Select(order => new OrderResponse(
                order.Id, order.UserId, order.OrderItems.Select(orderItem => new OrderItemResponse(
                    orderItem.Id, orderItem.ProductId, orderItem.Quantity, orderItem.TotalPrice, orderItem.UnitPrice
                    )).ToList(),
                order.CreatedAt,
                order.TotalAmount,
                order.OrderStatus
                )).ToList();

            return ServiceResult<IEnumerable<OrderResponse>>.Success(orderResponse, HttpStatusCode.OK);
        }
    }
}