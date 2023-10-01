using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    /// <summary>
    /// Interface para os dados e métodos da tabela 'Products'
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retorna uma lista de produtos com dados paginados
        /// </summary>
        /// <param name="page">Numero da página desejada. Se nao passar, será considerado 0</param>
        Paginacao<Product> ListProducts(int page);
    }
}