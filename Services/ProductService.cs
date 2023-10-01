using ProvaPub.Commom;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    /// <summary>
    /// Classe para os dados e métodos da tabela 'Products'
    /// </summary>
    public class ProductService : IProductService
    {
        TestDbContext _ctx;

        public ProductService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// Retorna uma lista de produtos com dados paginados
        /// </summary>
        /// <param name="page">Numero da página desejada. Se nao passar, será considerado 0</param>
        /// <returns>Retorna os dados da tabela 'Products' de forma paginada</returns>
        public Paginacao<Product> ListProducts(int page)
        {
            return new PaginacaoService<Product>(_ctx.Products).Paginate(page);
        }
    }
}
