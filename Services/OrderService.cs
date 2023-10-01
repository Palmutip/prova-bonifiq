using AutoMapper;
using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Commom;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using System.ComponentModel.DataAnnotations;

namespace ProvaPub.Services
{
    /// <summary>
    ///  Classe responsável por executar os métodos associados à tabela 'Orders'
    /// </summary>
    public class OrderService : IOrderService
    {
        TestDbContext _ctx;
        private readonly IEnumerable<IPaymentMethod> _paymentMethods;
        private readonly ICustomerService _customerService;

        public OrderService(TestDbContext ctx, IEnumerable<IPaymentMethod> paymentMethods, ICustomerService customerService)
        {
            _ctx= ctx;
            _paymentMethods = paymentMethods;
            _customerService = customerService;
        }

        public async Task Post(decimal paymentValue, int customerId)
        {
            var newOrder = new Order
            {
                Value = paymentValue,
                CustomerId = customerId, 
                OrderDate = DateTime.Now
            };

            _ctx.Orders.Add(newOrder);

            await _ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Método responsavel por processar o pagamento conforme seu tipo.
        /// </summary>
        /// <param name="paymentMethod">Forma de pagmento. (pix, creditcard, paypal)</param>
        /// <param name="paymentValue">Valor do Pagamento</param>
        /// <param name="customerId">ID do cliente</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Order> ProcessPayment(string paymentMethod, decimal paymentValue, int customerId) 
        {
            var selectedPaymentMethod = _paymentMethods.FirstOrDefault(method => method.Name.Equals(paymentMethod, StringComparison.OrdinalIgnoreCase));

            if (selectedPaymentMethod == null)
                throw new InvalidOperationException("Método de pagamento não suportado.");

            await _customerService.CanPurchase(customerId, paymentValue);

            await Post(paymentValue, customerId);

            return await selectedPaymentMethod.PayOrder(paymentValue, customerId);
        }
    }
}
