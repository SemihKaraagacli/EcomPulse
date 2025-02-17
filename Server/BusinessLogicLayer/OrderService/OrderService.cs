using BusinessLogicLayer.OrderService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.BasketItemRepository;
using DataAccessLayer.BasketRepository;
using DataAccessLayer.Entities;
using DataAccessLayer.OrderItemRepository;
using DataAccessLayer.OrderRepository;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BusinessLogicLayer.OrderService;

public class OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, UserManager<AppUser> userManager, IUnitOfWork unitOfWork) : IOrderService
{
    public async Task<Result<string>> CreateOrder(OrderCreateRequest request)
    {
        var hasUser = await userManager.FindByIdAsync(request.UserId.ToString());
        if (hasUser is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "User not found");
        }
        var hasBasket = await basketRepository.GetAllAsync(request.UserId);
        if (hasBasket is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Basket not found");
        }
        var hasBasketItem = hasBasket.BasketItems;
        if (hasBasketItem is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Basket item not found");
        }
        var hasOrder = await orderRepository.GetAllOrder(request.UserId);
        if (hasOrder != null && hasOrder.Any(x => x.OrderStatus == "Pending"))
        {
            return Result<string>.Failure(HttpStatusCode.BadRequest, "Found an order pending processing");
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
                orderItemRepository.Create(orderItem); // Add OrderItem one by one
            }
            foreach (var basketItem in hasBasketItem)
            {
                basketItemRepository.Delete(basketItem); // Delete individual BasketItem
            }
            orderRepository.Create(newOrder);
            basketRepository.Delete(hasBasket);
        }
        await unitOfWork.CommitAsync();
        return "Sipariş oluşturuldu";
    }
    public async Task<Result<IEnumerable<OrderResponse>>> GetAllOrder(Guid userId)
    {
        var hasUser = await userManager.FindByIdAsync(userId.ToString());
        if (hasUser is null)
        {
            return Result<IEnumerable<OrderResponse>>.Failure(HttpStatusCode.NotFound, "User not found.");
        }
        var allOrder = await orderRepository.GetAllOrder(userId);
        var orderResponse = allOrder.Select(order => new OrderResponse(
            order.Id, order.UserId, order.OrderItems!.Select(orderItem => new OrderItemResponse(
                orderItem.Id, orderItem.ProductId, orderItem.Quantity, orderItem.TotalPrice, orderItem.UnitPrice
                )).ToList(),
            order.CreatedAt,
            order.TotalAmount,
            order.OrderStatus
            )).ToList();

        return orderResponse;
    }
    public async Task<Result<OrderResponse>> GetByIdOrder(Guid orderId)
    {
        var hasOrder = await orderRepository.GetByIdOrder(orderId);
        if (hasOrder is null)
        {
            return Result<OrderResponse>.Failure(HttpStatusCode.NotFound, "Order not found.");
        }
        var orderResponse = new OrderResponse(hasOrder.Id, hasOrder.UserId, hasOrder.OrderItems!.Select(x => new OrderItemResponse(x.Id, x.ProductId, x.Quantity, x.TotalPrice, x.UnitPrice)).ToList(), hasOrder.CreatedAt, hasOrder.TotalAmount, hasOrder.OrderStatus);
        return orderResponse;
    }
    public async Task<Result<string>> OrderDelete(Guid orderId)
    {
        var hasOrder = await orderRepository.GetByIdOrder(orderId);
        if (hasOrder is null)
        {
            return Result<string>.Failure(HttpStatusCode.NotFound, "Order not found.");
        }
        foreach (var item in hasOrder.OrderItems!)
        {
            orderItemRepository.Delete(item);
        }
        orderRepository.Delete(hasOrder);
        await unitOfWork.CommitAsync();
        return "Sipariş başarıyla silindi";
    }
}