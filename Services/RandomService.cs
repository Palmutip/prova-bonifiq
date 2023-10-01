using ProvaPub.Interfaces;

namespace ProvaPub.Services
{
    /// <summary>
    /// Classe responsável por gerar um numero aleatório
    /// </summary>
    public class RandomService : IRandomService
    {
        private readonly Random _random;
        private int _seed;

        public RandomService()
        {
            _seed = Guid.NewGuid().GetHashCode();
            _random = new Random(_seed);
        }

        /// <summary>
        /// Método responsável por retornar um numero inteiro aleatório
        /// </summary>
        /// <returns>Um numero intero aleatório cada vez que é instanciado</returns>
        public int GetRandom()
        {
            return _random.Next(100);
        }

    }
}
