using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Commom.Pagamentos.Metodos
{
    public class PixPaymentMethod : IPaymentMethod
    {
        public string Name => "pix";

        private readonly ICustomerService _customerService;

        public PixPaymentMethod(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<Order> PayOrder(decimal paymentValue, int customerId)
        {
            return await new PaymentMethod(_customerService).PayOrder(paymentValue, customerId);
        }
    }
}
