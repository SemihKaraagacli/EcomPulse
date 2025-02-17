using BusinessLogicLayer.PaymentService.Dtos;
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
    public async Task<Result<PaymentResponse>> PaymnetProcessing(PaymentCreateRequest request)
    {
        var hasOrder = await orderRepository.GetByIdOrder(request.OrderId);
        if (hasOrder is null)
        {
            return Result<PaymentResponse>.Failure(HttpStatusCode.NotFound, "Order not found.");
        }
        else if (hasOrder.OrderStatus is "Completed")
        {
            return Result<PaymentResponse>.Failure(HttpStatusCode.BadRequest, "The order has been paid.");
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
                return Result<PaymentResponse>.Failure(HttpStatusCode.NotFound, "Credit Card not found.");
            }
            if (hasOrder.TotalAmount > hasCredirCard.AvailableBalance)
            {
                return Result<PaymentResponse>.Failure(HttpStatusCode.BadRequest, "The budget for shopping is insufficient.");
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
        return paymentResponse;
    }
}
