using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Commom.Pagamentos
{
    /// <summary>
    /// Classe responsável por processar o pagamento, indepente da método
    /// </summary>
    public class PaymentMethod 
    {
        private readonly ICustomerService _customerService;

        public PaymentMethod(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Método responsável por processar o pagamento
        /// </summary>
        /// <param name="paymentValue">Valor do pagamento</param>
        /// <param name="customerId">ID do cliente</param>
        /// <returns></returns>
        public async Task<Order> PayOrder(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order()
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.Now,
                Customer = await _customerService.GetById(customerId)
            });
        }
    }
}
