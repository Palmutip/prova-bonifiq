using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Commom.Pagamentos.Metodos
{
    public class PayPalPaymentMethod : IPaymentMethod
    {
        public string Name => "paypal";

        private readonly ICustomerService _customerService;

        public PayPalPaymentMethod(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Order> PayOrder(decimal paymentValue, int customerId)
        {
            return await new PaymentMethod(_customerService).PayOrder(paymentValue, customerId);
        }
    }
}
