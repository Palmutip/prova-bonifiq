using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    /// <summary>
    /// Interface responsável por idetnficiar a forma de pagamento conforme as classes que herdam dela 
    /// </summary>
    public interface IPaymentMethod
    {
        /// <summary>
        /// Propriedade que recebe a string que identifica a forma de pagamento
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Método responsável por processar o pagamento
        /// </summary>
        /// <param name="paymentValue">Valor do pagamento</param>
        /// <param name="customerId">ID do cliente</param>
        Task<Order> PayOrder(decimal paymentValue, int customerId);
    }
}
