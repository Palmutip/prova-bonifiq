using Microsoft.AspNetCore.Mvc;
using ProvaPub.Interfaces;

namespace ProvaPub.Controllers
{
    /// <summary>
    /// Ao rodar o código abaixo o serviço deve retornar um número inteiro diferente
    /// </summary>
    [ApiController]
	[Route("[controller]")]
	public class Parte1Controller :  ControllerBase
	{
		private readonly IRandomService _randomService;

		public Parte1Controller(IRandomService randomService)
		{
			_randomService = randomService;
		}

		/// <summary>
		/// Rota responsável por devolver um numero inteiro diferente a cada chamada.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public int Index()
		{
			return _randomService.GetRandom();
		}
	}
}
