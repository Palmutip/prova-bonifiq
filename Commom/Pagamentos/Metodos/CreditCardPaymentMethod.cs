using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Commom.Pagamentos.Metodos
{
    public class CreditCardPaymentMethod : IPaymentMethod
    {
        public string Name => "creditcard";

        private readonly ICustomerService _customerService;

        public CreditCardPaymentMethod(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Order> PayOrder(decimal paymentValue, int customerId)
        {
            return await new PaymentMethod(_customerService).PayOrder(paymentValue, customerId);
        }
    }
}
