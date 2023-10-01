using ProvaPub.Models;
namespace ProvaPub.Interfaces
{
    /// <summary>
    /// Interface para os dados e métodos da tabela 'Customers'
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Método responsável por retornar uma ViewModel com os dados do cliente que o usuário pode ver.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        Task<Customer> GetById(int customerId);
        
        /// <summary>
        /// Método responsável por validar se o Cliente pode realizar uma compra.
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <param name="purchaseValue">Valor da Compra</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
        
        /// <summary>
        /// Retorna uma lista de clientes com dados paginados
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        Paginacao<Customer> ListCustomers(int page);
    }
}