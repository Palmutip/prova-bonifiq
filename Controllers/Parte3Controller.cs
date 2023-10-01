using Microsoft.AspNetCore.Mvc;
using ProvaPub.Commom;
using ProvaPub.Interfaces;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

namespace ProvaPub.Controllers
{
	/// <summary>
	/// Rota responsável por solicitar a encomenda
	/// </summary>
	[ApiController]
	[Route("[controller]")]
	public class Parte3Controller :  ControllerBase
	{
        private readonly IOrderService _orderService;

        public Parte3Controller(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// responsável por solicitar a encomenda e processar o pagamento
        /// </summary>
        /// <param name="paymentMethod">string referente a forma de pagamento utilizada. (pix, creditcard ou paypal)</param>
        /// <param name="paymentValue">valor da encomenda / valor pago</param>
        /// <param name="customerId">ID do cliente</param>
        /// <returns></returns>
        [HttpGet("orders")]
		public async Task<IActionResult> PlaceOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			try
			{
				var result = await _orderService.ProcessPayment(paymentMethod, paymentValue, customerId);
                return Ok(result);
            }
            catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
        }
	}
}
