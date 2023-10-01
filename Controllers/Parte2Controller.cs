using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Rota com dois métodos referentes à tabela 'Customers'
    /// </summary>	
    [ApiController]
	[Route("[controller]")]
	public class Parte2Controller :  ControllerBase
	{
		private readonly ICustomerService _customerService;
        private readonly IProductService _productService;

        public Parte2Controller(ICustomerService customerService, IProductService productService)
		{
			_customerService = customerService;
			_productService = productService;
		}

        /// <summary>
        /// Retorna uma lista de produtos com os dados paginados
        /// </summary>
        /// <param name="page">Numero da página desejada. Se nao passar, será considerado 0</param>
        /// <returns></returns>
        [HttpGet("products")]
		public Paginacao<Product> ListProducts(int page)
		{
			return _productService.ListProducts(page);
		}

        /// <summary>
        /// Retorna uma lista de clientes com os dados paginados
        /// </summary>
        /// <param name="page">Numero da página desejada. Se nao passar, será considerado 0</param>
        /// <returns></returns>
        [HttpGet("customers")]
		public Paginacao<Customer> ListCustomers(int page)
		{
			return _customerService.ListCustomers(page);
		}
	}
}
