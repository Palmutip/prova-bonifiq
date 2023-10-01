using Microsoft.AspNetCore.Mvc;
using ProvaPub.Commom.Pagamentos;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Rota criada para realizar a validação da compra
    /// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte4Controller :  ControllerBase
	{
        private readonly ICustomerService _customerService;

        public Parte4Controller(ICustomerService customerService)
		{
            _customerService = customerService;
        }

        /// <summary>
        /// Rota responsável por validar se o cliente pode realizar uma compra
        /// </summary>
        /// <param name="customerId">ID do cliente</param>
        /// <param name="purchaseValue">Valor da compra</param>
        /// <returns></returns>
        [HttpGet("CanPurchase")]
		public async Task<IActionResult> CanPurchase(int customerId, decimal purchaseValue)
		{
			try
			{
                var result = await _customerService.CanPurchase(customerId, purchaseValue);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
		}
	}
}
