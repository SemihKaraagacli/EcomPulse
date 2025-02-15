﻿using BusinessLogicLayer.PaymentService.Dtos;
using BusinessLogicLayer.UnitOfWork;
using DataAccessLayer.CreditCardRepository;
using DataAccessLayer.Entities;
using DataAccessLayer.OrderRepository;
using DataAccessLayer.PaymentRepository;
using DataAccessLayer.ProductRepository;
using System.Net;

namespace BusinessLogicLayer.PaymentService;

public class PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, IOrderRepository orderRepository, ICreditCardRespository creditCardRespository, IProductRepository productRepository) : IPaymentService
{
    public async Task<ServiceResult<PaymentResponse>> PaymnetProcessing(PaymentCreateRequest request)
    {
        var hasOrder = await orderRepository.GetByIdOrder(request.OrderId);
        if (hasOrder is null)
        {
            return ServiceResult<PaymentResponse>.Fail("Order not found.", HttpStatusCode.NotFound);
        }
        else if (hasOrder.OrderStatus is "Completed")
        {
            return ServiceResult<PaymentResponse>.Fail("The order has been paid.", HttpStatusCode.BadRequest);
        }
        var newPayment = new Payment();
        if (request.PaymentMethod == "Cash")
        {
            newPayment.OrderId = request.OrderId;
            newPayment.PaymentDate = DateTime.Now;
            newPayment.PaymentMethod = request.PaymentMethod;
            newPayment.Amount = hasOrder.TotalAmount;
            newPayment.PaymentStatus = "Completed";

            var orderItem = hasOrder.OrderItems;
            foreach (var item in orderItem!)
            {
                item.Product!.Stock -= item.Quantity;
                productRepository.Update(item.Product);
            }
            hasOrder.OrderStatus = newPayment.PaymentStatus;
            paymentRepository.Create(newPayment);
            orderRepository.Update(hasOrder);
        }
        else
        {
            var hasCredirCard = await creditCardRespository.GetById(request.CreditCardId);
            if (hasOrder is null)
            {
                return ServiceResult<PaymentResponse>.Fail("Credit Card not found.", HttpStatusCode.NotFound);
            }
            if (hasOrder.TotalAmount > hasCredirCard.AvailableBalance)
            {
                return ServiceResult<PaymentResponse>.Fail("The budget for shopping is insufficient.", HttpStatusCode.BadRequest);
            }
            newPayment.OrderId = request.OrderId;
            newPayment.PaymentDate = DateTime.Now;
            newPayment.PaymentMethod = request.PaymentMethod;
            newPayment.Amount = hasOrder.TotalAmount;
            newPayment.PaymentStatus = "Completed";
            newPayment.CreditCardId = request.CreditCardId;

            hasCredirCard.AvailableBalance -= newPayment.Amount;
            creditCardRespository.Update(hasCredirCard);

            var orderItem = hasOrder.OrderItems;
            foreach (var item in orderItem!)
            {
                item.Product!.Stock -= item.Quantity;
                productRepository.Update(item.Product);
            }
            hasOrder.OrderStatus = newPayment.PaymentStatus;
            paymentRepository.Create(newPayment);
            orderRepository.Update(hasOrder);
        }
        await unitOfWork.CommitAsync();

        var paymentResponse = new PaymentResponse(
            newPayment.Id,
            newPayment.OrderId,
            newPayment.Amount,
            newPayment.PaymentDate,
            newPayment.PaymentMethod,
            newPayment.PaymentStatus,
            newPayment.CreditCardId
            );
        return ServiceResult<PaymentResponse>.Success(paymentResponse, HttpStatusCode.OK);
    }
}
