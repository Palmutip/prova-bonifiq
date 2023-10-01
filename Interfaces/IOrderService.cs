using ProvaPub.Models;
using ProvaPub.Services;

namespace ProvaPub.Interfaces
{
    /// <summary>
    /// Interface para os dados e métodos da tabela 'Orders'
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Método responsavel por processar o pagamento conforme seu tipo.
        /// </summary>
        /// <param name="paymentMethod">Forma de pagmento. (pix, creditcard, paypal)</param>
        /// <param name="paymentValue">Valor do Pagamento</param>
        /// <param name="customerId">ID do cliente</param>
        Task<Order> ProcessPayment(string paymentMethod, decimal paymentValue, int customerId);
        Task Post(decimal paymentValue, int customerId);
    }
}