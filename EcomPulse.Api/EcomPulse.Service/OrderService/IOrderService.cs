﻿using EcomPulse.Service.OrderService.Dtos;

namespace EcomPulse.Service.OrderService
{
    public interface IOrderService
    {
        Task<ServiceResult> CreateOrder(OrderCreateRequest request);
        Task<ServiceResult<IEnumerable<OrderResponse>>> GetAllOrder(Guid userId);
        Task<ServiceResult<OrderResponse>> GetByIdOrder(Guid OrderId);
        Task<ServiceResult> OrderDelete(Guid orderId);
    }
}